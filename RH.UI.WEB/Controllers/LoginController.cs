using Microsoft.AspNetCore.Mvc;
using RH.Models;
using RH.UI.WEB.Models;
using System;
using RH.BLL;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RH.UI.WEB.Controllers
{

    [AllowAnonymous]
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            var Onj = TempData["erroLogin"];
            if(Onj != null) 
            ViewBag.Message = TempData["erroLogin"].ToString();
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Logar(string nome, string senha)
        {
            Usuario usuario = null;
            if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(senha))
            {

                LoginBLL loginBLL = new LoginBLL();
                 usuario =loginBLL.Login(nome, senha);

            }
            if(usuario!=null) {

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usuario.usuario)
                };

                var usuarioIdentidade = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);

      

                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index","Home");  
            
            }

            ModelState.AddModelError(string.Empty, "Login Inválido");
            TempData["erroLogin"] = "1";
            return RedirectToAction("Index","Login");

        }


        [HttpPost]
        public IActionResult Registrar([FromForm]Login  login) {

          
            if (ModelState.IsValid)
            {
                Usuario usuarios = new Usuario();
                LoginBLL loginBLL = new LoginBLL();
                usuarios.usuario = login.usuario;
                usuarios.senha = login.senha;
                usuarios.email = login.email;


              int reposta=  loginBLL.Registrar(usuarios);   
            }
            TempData["erroLogin"] = "2";
            return RedirectToAction("Index", "Login");

        }
    }
}
