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
    }
}