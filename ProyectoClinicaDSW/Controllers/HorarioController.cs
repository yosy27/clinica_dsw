using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoClinicaDSW.Models;
using ProyectoClinicaDSW.Repositorio;
using ProyectoClinicaDSW.Repositorio.DatosSQL;

namespace ProyectoClinicaDSW.Controllers
{
    public class HorarioController : Controller
    {
        IHorario _Hor;
        IDiaSemana _Dia;

        public HorarioController()
        {
            _Hor = new HorarioSQL();
            _Dia = new DiaSemanaSQL();
        }

        #region Registrar
        public async Task<IActionResult> Create()
        {
            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
            return View(await Task.Run(() => new Horario()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Horario hor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia", hor.IdDia);
            }
            ViewBag.mensaje = _Hor.RegistrarHorario(hor);

            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia", hor.IdDia);
            return View(await Task.Run(() => hor));
        }
        #endregion
    }
}
