using Application.Common.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Admins.Locations.Commands
{
    public class CreateLocationCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImagePath { get; set; }
        public string Address { get; set; }
        public double XCordinate { get; set; }
        public double YCordinate { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Result>
    {
        private readonly IApplicationEFContext _context;
        private readonly ILogger<CreateLocationCommandHandler> _logger;

        public CreateLocationCommandHandler(IApplicationEFContext context,
                                            ILogger<CreateLocationCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existLocation = await _context.Locations
                                          .Where(q => EF.Functions.Like(q.Address, $"%{request.Address}%"))
                                          .FirstOrDefaultAsync(cancellationToken);

                if (existLocation != null)
                    return Result.Failure("Локация с таким адресом уже существует");

                var location = new Location()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Address = request.Address,
                    XCordinate = request.XCordinate,
                    YCordinate = request.YCordinate,
                    Status = LocationStatus.Active
                };

                _context.Locations.Add(location);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success("Успешно сохранено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Location create failed with error");
                return Result.Failure("Возникли ошибки создании");
            }
        }
    }
}