using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RH.BLL;
using RH.Models;
using RH.UI.WEB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.UI.WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //  ViewBag.Nome = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            DadosPessoaisForm1BLL dadosPessoaisForm1BLL = new DadosPessoaisForm1BLL();
            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            ViewBag.IdUsuarioDadosPessoais = dadosPessoaisForm1BLL.VerificarDadosPessoaisForm1(int.Parse(UserID));
          

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
