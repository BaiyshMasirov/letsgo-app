using Application.MediatR.Users.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Infrastructure
{
    public class UsersCountViewComponent : ViewComponent
    {
        private IMediator _mediator;

        protected IMediator Mediator
          => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["UsersCount"] = await Mediator.Send(new GetUsersCountQuery());
            return View();
        }
    }
}