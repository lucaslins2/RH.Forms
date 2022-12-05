using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.BLL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.UI.WEB.Controllers
{
    [Authorize(Policy = "Admistrator")]
    public class PainelController : Controller
    {
        public IActionResult Index()
        {

            CanditadoBLL canditadoBLL = new CanditadoBLL();
            CargoBLL cargoBLL = new CargoBLL();
            var cargos = cargoBLL.GetCargosBLL(0);

            ViewBag.cargos= cargos;
            var  listaCanditados= canditadoBLL.GetCanditados();
            ViewBag.cidades = canditadoBLL.GetCidades();



            return View(listaCanditados);
        }

        [HttpPost]
        public async Task<IActionResult> Pesquisar([FromBody]Filtros filtros) {


            CanditadoBLL canditadoBLL = new CanditadoBLL();
            var listaCanditados = canditadoBLL.GetCanditadosPesquisa(filtros);
            return PartialView("_Lista", listaCanditados);
        }

        public async Task<IActionResult> Visualizar(int id) {
            var nomeUsuario = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, nomeUsuario),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Sid, id.ToString()),
                };

                var usuarioIdentidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(principal, props);
       


            return RedirectToAction("Index", "Home");
        
        }

        public IActionResult GetCargos(int id)
        {

            CargoBLL cargoBLL = new CargoBLL();
            var cargos = cargoBLL.GetCargosBLL(id);
            string SelectCargos = "";
            foreach (var item in cargos)
            {
                SelectCargos += "'" + item.idCargo + "':'" + item.cargo + "',";


            }
            SelectCargos = SelectCargos.Remove(SelectCargos.Length - 1);
            return Json(SelectCargos);
        }
    }


   
}
