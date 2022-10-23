using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}