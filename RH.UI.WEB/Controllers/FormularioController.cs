﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RH.BLL;
using RH.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.UI.WEB.Controllers
{
    public class FormularioController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {

            List<SelectListItem> itemsCargos = new List<SelectListItem>();
            CargoBLL cargoBLL = new CargoBLL();
            var cargos = cargoBLL.GetCargosBLL();
            foreach (var item in cargos) {
                itemsCargos.Add(new SelectListItem() { Text = item.cargo, Value = item.idCargo.ToString(), Selected = false });
            }

            SelectList selectCargos = new SelectList(itemsCargos, "Value", "Text", "0");
            ViewBag.selectCargos = selectCargos;
            List<SelectListItem> itemsGrau = new List<SelectListItem>();
            GrauDeEstudoBLL grauDeEstudoBLL = new GrauDeEstudoBLL();
            var gradu = grauDeEstudoBLL.GetGrauDeEstudos();
            foreach (var item in gradu) {
                itemsGrau.Add(new SelectListItem() { Text = item.graudeestudo, Value = item.idgraudeestudo.ToString(), Selected = false });
            }
            SelectList selectGrau = new SelectList(itemsGrau, "Value", "Text", "0");
            ViewBag.selectGrau = selectGrau;

            List<SelectListItem> itemsSoleteiro = new List<SelectListItem>();
            EstadoCivilBLL estadoCivilBLL = new EstadoCivilBLL();

            foreach (var item in estadoCivilBLL.GetEstadosCivies())
            {
                itemsSoleteiro.Add(new SelectListItem() { Text = item.estadocivil, Value = item.idestadocivil.ToString(), Selected = false });

            }
            SelectList selectEstadoCivil = new SelectList(itemsSoleteiro, "Value", "Text", "0");
            ViewBag.selectEstadoCivil = selectEstadoCivil;


            List<SelectListItem> itemsUF = new List<SelectListItem>();
            EstadoBLL estadoBLL = new EstadoBLL();
            foreach (var item in estadoBLL.GetEstados())
            {
                itemsUF.Add(new SelectListItem() { Text = item.estado, Value = item.idUF.ToString(), Selected = false });

            }
            SelectList selectEstado = new SelectList(itemsUF, "Value", "Text", "0");
            ViewBag.selectEstados = selectEstado;

            //Acessa ao banco para pega se informacaoes eciste
            //DadosPessoaisForm1 dadosPessoaisForm1 = new DadosPessoaisForm1();
            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            DadosPessoaisForm1BLL DadosPessoaisForm1BLL = new DadosPessoaisForm1BLL();
            var DadosPessoaisForm1 = DadosPessoaisForm1BLL.GetDadosPessoaisForm1(int.Parse(UserID));

            //DadosPessoaisForm1.categoriasCNH = new List<string> {"A","B","C" };           // dadosPessoaisForm1.idCargo = 2;
      
            return View(DadosPessoaisForm1);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarForm1(DadosPessoaisForm1 dadosPessoaisForm1) {

            //Pega id do usaurio no cookies
            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            DadosPessoaisForm1BLL DadosPessoaisForm1BLL = new DadosPessoaisForm1BLL();
            var DadosPessoaisForm1 = DadosPessoaisForm1BLL.GetDadosPessoaisForm1(int.Parse(UserID));
            int Resultado = 0;
            if (DadosPessoaisForm1 != null) {

               Resultado =  DadosPessoaisForm1BLL.CadastrarDadosPessoaisForm1(dadosPessoaisForm1, int.Parse(UserID), 2);

            }
            else
            {
                 Resultado = DadosPessoaisForm1BLL.CadastrarDadosPessoaisForm1(dadosPessoaisForm1, int.Parse(UserID), 1);

            }

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, dadosPessoaisForm1.nomecompleto),
                    new Claim(ClaimTypes.Sid, UserID.ToString()),
                };

            var usuarioIdentidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
            var props = new AuthenticationProperties();



            await HttpContext.SignInAsync(principal, props);

            return RedirectToAction("Index", "Home");
        
        }
    }
}