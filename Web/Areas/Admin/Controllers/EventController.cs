using Application.MediatR.Admins.Events.Commands;
using Application.MediatR.Admins.Events.Queries.GetEvents;
using Application.MediatR.Admins.Locations.Queries.GetLocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class EventController : BaseController
    {
        public async Task<IActionResult> Index(GetEventsQuery query)
        {
            ViewData["Events"] = await Mediator.Send(query);
            return View(query);
        }

        public async Task<IActionResult> Create()
        {
            await FillLocations();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (result.Succeed)
                {
                    foreach (var message in result.Messages) Notyf.Success(message);
                    return RedirectToAction(nameof(Index));
                }
                foreach (var message in result.Messages) Notyf.Error(message);
            }
            await FillLocations();
            return View(command);
        }

        private async Task FillLocations()
        {
            var events = await Mediator.Send(new GetActiveLocationsQuery());
            ViewData["Locations"] = new SelectList(events, "Id", "Name");
        }
    }
}