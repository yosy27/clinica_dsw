using Microsoft.AspNetCore.Mvc;

namespace ProyectoClinicaDSW.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
