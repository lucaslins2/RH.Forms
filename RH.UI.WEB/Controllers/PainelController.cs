﻿using Microsoft.AspNetCore.Authentication;
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
            var  listaCanditados= canditadoBLL.GetCanditados();
            return View(listaCanditados);
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
    }
}