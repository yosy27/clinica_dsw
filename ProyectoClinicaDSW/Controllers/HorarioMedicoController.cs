using Microsoft.AspNetCore.Mvc;
using ProyectoClinicaDSW.Repositorio.DatosSQL;
using ProyectoClinicaDSW.Repositorio;

namespace ProyectoClinicaDSW.Controllers
{
    public class HorarioMedicoController : Controller
    {
        IHorarioMedico _Hor;
        IMedico _Med;
        IDiaSemana _Dia;
        public HorarioMedicoController()
        {
            _Med = new MedicoSQL();
            _Dia = new DiaSemanaSQL();
            _Hor = new HorarioMedicoSQL();
        }

        public async Task<IActionResult> ListadoHorario()
        {
            return View(await Task.Run(() => _Hor.ListaHorarioMedico()));
        }

    }
}
