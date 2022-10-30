using Application.MediatR.Admins.Accounts.Commands;
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

        public async Task<IActionResult> UserLock(GetAdminByIdQuery query)
        {
            var admin = await Mediator.Send(query);

            if (admin == null)
            {
                Notyf.Error("По данному идентификатору сотрудник не найден!");
                return RedirectToAction(nameof(Index));
            }

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Lock(LockoutAdminCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Succeed)
                foreach (var message in result.Messages) Notyf.Error(message);
            else
                foreach (var message in result.Messages) Notyf.Success(message);

            return RedirectToAction(nameof(Index));
        }
    }
}