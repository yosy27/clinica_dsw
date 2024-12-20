using Microsoft.AspNetCore.Mvc;
using ProyectoClinicaDSW.Repositorio;
using ProyectoClinicaDSW.Repositorio.DatosSQL;

namespace ProyectoClinicaDSW.Controllers
{
    public class CitaController : Controller
    {
        IMedico _Med;
        IPaciente _Pac;
        IEstados _Est;
        ICita _Cit;
        public  CitaController()
        {
            _Med = new MedicoSQL();
            _Pac = new PacienteSQL();
            _Est = new EstadoSQL();
            _Cit = new CitaSQL();
        }

        public async Task<IActionResult> FilterCita(string dni)
        {
            return View(await Task.Run(() => _Cit.FilterCita(dni)));
        }


    }
}
