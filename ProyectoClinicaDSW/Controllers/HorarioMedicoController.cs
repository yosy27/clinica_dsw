using Microsoft.AspNetCore.Mvc;
using ProyectoClinicaDSW.Repositorio.DatosSQL;
using ProyectoClinicaDSW.Repositorio;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoClinicaDSW.Models;

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

        public async Task<IActionResult> FilterHorario(string nomMedico)
        {
            return View(await Task.Run(() => _Hor.FilterHorario(nomMedico)));
        }


        #region Registrar
        public async Task<IActionResult> Create()
        {
            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
            ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico");
            return View(await Task.Run(() => new HorarioMedico()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(HorarioMedico hor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
                ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "NombreCompleto");
            }
            ViewBag.mensaje = _Hor.RegistrarHorario(hor);

            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
            ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "NombreCompleto");
            return View(await Task.Run(() => hor));
        }
        #endregion
    }
}
