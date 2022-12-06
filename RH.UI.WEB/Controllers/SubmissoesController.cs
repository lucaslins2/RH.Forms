using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RH.UI.WEB.Controllers
{
    [Authorize]
    public class SubmissoesController : Controller
    {
       
        public IActionResult Index()
        {
            CargoBLL cargoBLL = new CargoBLL();
            var UserID = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var subsmissoes = cargoBLL.GetSubmissoes(int.Parse(UserID));
            return View(subsmissoes);
        }
    }
}
