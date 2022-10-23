using AspNetCoreHero.ToastNotification.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private IMediator _mediator;
        private INotyfService _notyfService;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected INotyfService Notyf => _notyfService ??= HttpContext.RequestServices.GetService<INotyfService>();
    }
}