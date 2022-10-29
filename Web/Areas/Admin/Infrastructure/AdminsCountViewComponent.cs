using Application.MediatR.Admins.Accounts.Queries.GetAdmins;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Infrastructure
{
    public class AdminsCountViewComponent : ViewComponent
    {
        private IMediator _mediator;

        protected IMediator Mediator
          => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["AdminsCount"] = await Mediator.Send(new GetAdminsCountQuery());
            return View();
        }
    }
}