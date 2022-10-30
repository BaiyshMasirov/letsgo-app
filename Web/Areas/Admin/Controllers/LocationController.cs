using Application.MediatR.Admins.Locations.Commands;
using Application.MediatR.Admins.Locations.Queries.GetLocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class LocationController : BaseController
    {
        public async Task<IActionResult> Index(GetLocationsQuery query)
        {
            ViewData["Locations"] = await Mediator.Send(query);
            return View(query);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationCommand command)
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
            return View(command);
        }
    }
}