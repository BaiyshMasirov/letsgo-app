using Application.Common.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Admins.Events.Commands
{
    public class CreateEventCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IFormFile Poster { get; set; }
        public int AgeLimit { get; set; }
        public decimal MinPrice { get; set; }
        public Guid LocationId { get; set; }
        public IList<TicketDto> Tickets { get; set; }
    }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result>
    {
        private readonly IApplicationEFContext _context;
        private readonly IFileService _fileService;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(IApplicationEFContext context,
                                         IFileService fileService,
                                         ILogger<CreateEventCommandHandler> logger)
        {
            _context = context;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _context.BeginTransactionAsync();
                var existEvent = await _context.Events.FirstOrDefaultAsync(x => x.LocationId == request.LocationId
                                                                             && x.StartDate.Date == request.StartDate
                                                                             && x.EndDate.Date == request.EndDate);

                if (existEvent != null)
                    return Result.Failure("Событие с заданной локацией и на заданную дату уже существует");

                var ev = new Event()
                {
                    Name = request.Name,
                    Description = request.Description,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    AgeLimit = request.AgeLimit,
                    MinPrice = request.MinPrice,
                    Count = request.Tickets.Count,
                    LocationId = request.LocationId,
                    Status = EventStatus.Active,
                    ImagePath = await _fileService.AddFileAsync(request.Poster)
                };
                await GenerateTicketByEventId(ev.Id, request.Tickets, cancellationToken);
                await _context.CommitTransactionAsync();
                return Result.Success("Успешно сохранено");
            }
            catch (Exception ex)
            {
                await _context.RollbackTransactionAsync();

                _logger.LogError(ex, $"Event creation failed with error");
                return Result.Failure("Проиозошла ошибка при сохранении");
            }
        }

        private async Task GenerateTicketByEventId(Guid eventId, IList<TicketDto> tickets, CancellationToken token)
        {
            foreach (var tick in tickets)
            {
                var ticket = new Ticket()
                {
                    EventId = eventId,
                    PlaceNumber = tick.PlaceNumber,
                    Price = tick.Price,
                };
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync(token);
            }
        }
    }
}