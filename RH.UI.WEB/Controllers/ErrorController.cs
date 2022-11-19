using Microsoft.AspNetCore.Mvc;

namespace RH.UI.WEB.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
