using Microsoft.AspNetCore.Authentication.Cookies;
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
using System;
using RH.DAL;

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

            ExperienciaAnteriorDAL experienciaAnteriorDAL = new ExperienciaAnteriorDAL();
            var ListaExperiencia = experienciaAnteriorDAL.GetExperienciasAnteriores(int.Parse(UserID));
            if (ListaExperiencia.Count > 0)
            {
                int contador = 0;
                foreach (var lista in ListaExperiencia)
                {
                    contador++;
                    DadosPessoaisForm1.idExperienciaAnterior.Add(lista.idExperienciaAnterior);
                    DadosPessoaisForm1.empresa.Add(lista.empresa);
                    DadosPessoaisForm1.telefoneEmpresa.Add(lista.telefoneEmpresa);
                    DadosPessoaisForm1.contato.Add(lista.contato);
                    DadosPessoaisForm1.setor.Add(lista.setor);
                    DadosPessoaisForm1.cargoExercido.Add(lista.cargoExercido);
                    DadosPessoaisForm1.enderecoEmpresa.Add(lista.enderecoEmpresa);
                    DadosPessoaisForm1.dataAdmissao.Add(lista.dataAdmissao);
                    DadosPessoaisForm1.dataDemissao.Add(lista.dataDemissao);
                    DadosPessoaisForm1.motivoSaida.Add(lista.motivoSaida);
                    DadosPessoaisForm1.QtdExp.Add(contador - 1);

                }


            }

            if(DadosPessoaisForm1 == null)
                DadosPessoaisForm1 = new DadosPessoaisForm1();

            //DadosPessoaisForm1.categoriasCNH = new List<string> {"A","B","C" };           // dadosPessoaisForm1.idCargo = 2;

            return View(DadosPessoaisForm1);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarForm1(DadosPessoaisForm1 dadosPessoaisForm1) {

            //Pega id do usaurio no cookies
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            DadosPessoaisForm1BLL DadosPessoaisForm1BLL = new DadosPessoaisForm1BLL();
            var DadosPessoaisForm1 = DadosPessoaisForm1BLL.GetDadosPessoaisForm1(int.Parse(UserID));
            int Resultado = 0;
            List<ExperienciaAnterior> experienciaAnteriores = new List<ExperienciaAnterior>();
            if (dadosPessoaisForm1.QtdExp != null) {
            foreach (int number in dadosPessoaisForm1.QtdExp)
            {
                ExperienciaAnterior experienciaAnterior = new ExperienciaAnterior();

                if (dadosPessoaisForm1.idExperienciaAnterior.Count >= number && dadosPessoaisForm1.idExperienciaAnterior.Count>0)
                experienciaAnterior.idExperienciaAnterior = dadosPessoaisForm1.idExperienciaAnterior[number];
                else experienciaAnterior.idExperienciaAnterior = 0;

                if (dadosPessoaisForm1.empresa.Count >= number+1 && dadosPessoaisForm1.empresa.Count > 0)
                    experienciaAnterior.empresa = dadosPessoaisForm1.empresa[number];

                if (dadosPessoaisForm1.telefoneEmpresa.Count >= number+1 && dadosPessoaisForm1.telefoneEmpresa.Count > 0)
                    experienciaAnterior.telefoneEmpresa = dadosPessoaisForm1.telefoneEmpresa[number];

                if (dadosPessoaisForm1.contato.Count >= number+1 && dadosPessoaisForm1.contato.Count > 0)
                    experienciaAnterior.contato = dadosPessoaisForm1.contato[number];

                if (dadosPessoaisForm1.setor.Count >= number+1 && dadosPessoaisForm1.setor.Count > 0)
                    experienciaAnterior.setor = dadosPessoaisForm1.setor[number];

                if (dadosPessoaisForm1.cargoExercido.Count >= number+1 && dadosPessoaisForm1.cargoExercido.Count > 0)
                    experienciaAnterior.cargoExercido = dadosPessoaisForm1.cargoExercido[number];

                if (dadosPessoaisForm1.enderecoEmpresa.Count >= number+1 && dadosPessoaisForm1.enderecoEmpresa.Count > 0)
                    experienciaAnterior.enderecoEmpresa = dadosPessoaisForm1.enderecoEmpresa[number];

                if (dadosPessoaisForm1.dataAdmissao.Count >= number+1 && dadosPessoaisForm1.dataAdmissao.Count > 0)
                    experienciaAnterior.dataAdmissao = dadosPessoaisForm1.dataAdmissao[number];

                if (dadosPessoaisForm1.dataDemissao.Count >= number+1 && dadosPessoaisForm1.dataDemissao.Count > 0)
                    experienciaAnterior.dataDemissao = dadosPessoaisForm1.dataDemissao[number];

                if (dadosPessoaisForm1.motivoSaida.Count >= number+1 && dadosPessoaisForm1.motivoSaida.Count > 0)
                    experienciaAnterior.motivoSaida = dadosPessoaisForm1.motivoSaida[number];

                experienciaAnteriores.Add(experienciaAnterior);
            }
            }
            ExperienciaAnteriorBLL experienciaAnteriorBLL = new ExperienciaAnteriorBLL();
            if (experienciaAnteriores.Count > 0)
            {
                experienciaAnteriorBLL.DeletarExperienciasAnterior(int.Parse(UserID));
                experienciaAnteriorBLL.CadastrarExperienciasAnterior(experienciaAnteriores, int.Parse(UserID)); 
           
            }
            else
                experienciaAnteriorBLL.DeletarExperienciasAnterior(int.Parse(UserID));
  
            if (DadosPessoaisForm1 != null) {

               Resultado =  DadosPessoaisForm1BLL.CadastrarDadosPessoaisForm1(dadosPessoaisForm1, int.Parse(UserID), 2);

            }
            else
            {
                 Resultado = DadosPessoaisForm1BLL.CadastrarDadosPessoaisForm1(dadosPessoaisForm1, int.Parse(UserID), 1);

            }

            if (!string.IsNullOrEmpty(dadosPessoaisForm1.nomecompleto))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, dadosPessoaisForm1.nomecompleto),
                    new Claim(ClaimTypes.Sid, UserID.ToString()),
                };

                var usuarioIdentidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
                var props = new AuthenticationProperties();
          


            await HttpContext.SignInAsync(principal, props);
            }
            return RedirectToAction("Index", "Home");
        
        }
    }
}
