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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                    new Claim(ClaimTypes.Name, usuario.nome),
                    new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString()),
                };

                var usuarioIdentidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
                var props = new AuthenticationProperties();



                await HttpContext.SignInAsync(principal, props);
               // var userId = HttpContext.User.Claims.
                return RedirectToAction("Index","Home");  
            
            }

            ModelState.AddModelError(string.Empty, "Login Inválido");
            TempData["erroLogin"] = "1";
            return RedirectToAction("Index","Login");

        }
        public async Task <IActionResult> Sair() {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public IActionResult Registrar([FromForm]Login  login) {

          
            if (ModelState.IsValid)
            {
                Usuario usuarios = new Usuario();
                LoginBLL loginBLL = new LoginBLL();
                usuarios.cpf = login.cpf;
                usuarios.nome = login.nome;
                usuarios.senha = login.senha;
                usuarios.email = login.email;


              int reposta=  loginBLL.Registrar(usuarios);   
            }
            TempData["erroLogin"] = "2";
            return RedirectToAction("Index", "Login");

        }
    }
}
