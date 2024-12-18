using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoClinicaDSW.Models;
using ProyectoClinicaDSW.Repositorio;
using System.Security.Claims;

namespace ProyectoClinicaDSW.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuarioRepositorio;

        public UsuarioController(IUsuario usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usu)
        {
            if (usu == null || string.IsNullOrEmpty(usu.correoUsuario) || string.IsNullOrEmpty(usu.clave))
            {
                return BadRequest("Por favor, proporciona las credenciales completas.");
            }

            usu.MantenerActivo = Request.Form["MantenerActivo"] == "on";

            try
            {
                var user = await _usuarioRepositorio.Login(usu);

                if (user == null)
                {
                    TempData["Error"] = "Credenciales incorrectas o cuenta no registrada."; // Error para mostrar en la vista
                    return View();
                }

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.correoUsuario),
            new Claim(ClaimTypes.Name, user.Nombre)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = usu.MantenerActivo,
                    ExpiresUtc = usu.MantenerActivo
                        ? DateTimeOffset.UtcNow.AddMinutes(30)
                        : DateTimeOffset.UtcNow.AddSeconds(10)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

             
                TempData["Success"] = "Inicio de sesión exitoso!";

             
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }



    }
}
