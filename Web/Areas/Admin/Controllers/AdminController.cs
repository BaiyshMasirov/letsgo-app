using Application.MediatR.Admins.Accounts.Queries.GetAdmins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminController : BaseController
    {
        public async Task<IActionResult> Index(GetAdminsQuery query)
        {
            ViewData["Admins"] = await Mediator.Send(query);
            return View(query);
        }
    }
}