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

                if(nome.IndexOf("@")>0)
                usuario = loginBLL.Login(nome, senha);
                else
                usuario = loginBLL.Login(nome.Replace(".","").Replace("-",""), senha);

            }
            if(usuario!=null) {
                var claims = new List<Claim>();
                if (usuario.admin == 1)
                {
                    // claims.Add(new Claim("Store", "Admin"));
                    claims.Add(new Claim(ClaimTypes.Name, usuario.nome));
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
                    claims.Add(new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString()));

                }
                else {

                    claims.Add(new Claim(ClaimTypes.Name, usuario.nome));
                    claims.Add(new Claim(ClaimTypes.Role, "public"));
                    claims.Add(new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString()));

                }
            
          

                var usuarioIdentidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(usuarioIdentidade);
                var props = new AuthenticationProperties();



                await HttpContext.SignInAsync(principal, props);
                // var userId = HttpContext.User.Claims.
                if (usuario.admin == 1)
                {
                    return RedirectToAction("Index", "Painel");
                }
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
                usuarios.cpf = login.cpf.Replace(".", "").Replace("-","");
                usuarios.nome = login.nome;
                usuarios.senha = login.senha;
                usuarios.email = login.email;

                var userExist = loginBLL.VerificarEmailOuCPF(usuarios.email, usuarios.cpf) ;
            if (userExist == null) { 
              int reposta=  loginBLL.Registrar(usuarios);
                    TempData["erroLogin"] = "2";
                }
              else
                TempData["erroLogin"] = "3";
            }

       
            return RedirectToAction("Index", "Login");

        }
    }
}
