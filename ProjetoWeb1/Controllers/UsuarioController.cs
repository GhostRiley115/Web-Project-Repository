using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb1.Interfaces;
using ProjetoWeb1.Models;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace ProjetoWeb1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        //construtor
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        //View é sempre no método get
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (!ModelState.IsValid) return View(user);
            var usuario = _usuarioRepositorio.Validar(user.Email, user.Senha);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("NivelAcesso", usuario.Nivel),
                    new Claim("UsuarioId", usuario.Id.ToString())
                };
                var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identidade),
                    new AuthenticationProperties { IsPersistent = false });

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email ou senha invalido");
            return View(user);
        }
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
