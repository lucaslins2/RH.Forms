﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RH.BLL;
using RH.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RH.UI.WEB.Controllers
{
    public class QuestaoController : Controller
    {
        [Authorize]
        public IActionResult Index(int id)
        {

            if (id == 0)
               return RedirectToAction("Index", "Home");


            var admin = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value;

            CargoBLL cargoBLL = new CargoBLL();
            var cargo = cargoBLL.GetCargo(id);
            if (cargo.idvaga > 0 && admin!="admin")
            {
                TempData["erroCargo"] = 1;

                return RedirectToAction("Index", "Home");
            
            }
            ViewBag.nomeCargo = cargo.cargo;
            ViewBag.idCargo = cargo.idCargo;

            PerguntaBLL perguntaBLL = new PerguntaBLL();
            var listaPerguntas = perguntaBLL.GetPerguntas(id);
            return View(listaPerguntas);
        }
        [HttpPost]
        public IActionResult Salvar(List<Pergunta> perguntas, int idCargo) {

            PerguntaBLL perguntaBLL = new PerguntaBLL();

            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            perguntaBLL.SalvarReposta(perguntas, int.Parse(UserID));
            perguntaBLL.SalvarVagar(int.Parse(UserID), idCargo);
            return RedirectToAction("Home", "Index");
        }
    }
}
