using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.UI.WEB.Controllers
{
    [Authorize(Policy = "Admistrator")]
    public class PainelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
