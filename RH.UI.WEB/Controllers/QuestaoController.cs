using Microsoft.AspNetCore.Authorization;
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
                RedirectToAction("Index", "Home");

            CargoBLL cargoBLL = new CargoBLL();
            var cargo = cargoBLL.GetCargo(id);
            ViewBag.nomeCargo = cargo.cargo;

            PerguntaBLL perguntaBLL = new PerguntaBLL();
            var listaPerguntas = perguntaBLL.GetPerguntas(id);
            return View(listaPerguntas);
        }
        [HttpPost]
        public IActionResult Salvar(List<Pergunta> perguntas) {

            PerguntaBLL perguntaBLL = new PerguntaBLL();

            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            perguntaBLL.SalvarReposta(perguntas, int.Parse(UserID));
            return RedirectToAction("Home", "Index");
        }
    }
}
