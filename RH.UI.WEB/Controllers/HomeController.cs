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
            var admin = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            string UserIda = "";
            if(admin=="admin")
              UserIda=  HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;


            DadosPessoaisForm1BLL dadosPessoaisForm1BLL = new DadosPessoaisForm1BLL();
            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
           int? validadadospessoais  = dadosPessoaisForm1BLL.VerificarDadosPessoaisForm1(int.Parse(UserID));
         
            ViewBag.IdUsuarioDadosPessoais = validadadospessoais;
            CargoBLL cargoBLL = new CargoBLL();
            int verSubmissoes = cargoBLL.VerSubmissao(int.Parse(UserID));
            ViewBag.IdSubmissao = verSubmissoes;
           var Onj = TempData["erroCargo"];
            if (Onj != null)
                ViewBag.Message = TempData["erroCargo"].ToString();
            else
                ViewBag.Message = 0;
            if (validadadospessoais > 0) {
                List<Cargo> cargos = cargoBLL.GetCargosBLL(!String.IsNullOrEmpty(UserIda)? int.Parse(UserIda): 0);
                ViewBag.cargos = cargos;
            }
         



            return View();
        }
        [Authorize(Policy = "Admistrator")]
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
