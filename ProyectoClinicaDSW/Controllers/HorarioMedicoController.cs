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
            ViewBag.medicos = new SelectList(_Med.ListMedicoCb(), "idMedico", "nombreMedico");
            return View(await Task.Run(() => new HorarioMedico()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(HorarioMedico hor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
                ViewBag.medicos = new SelectList(_Med.ListMedicoCb(), "idMedico", "nombreMedico");
            }
            ViewBag.mensaje = _Hor.RegistrarHorario(hor);

            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
            ViewBag.medicos = new SelectList(_Med.ListMedicoCb(), "idMedico", "nombreMedico");
            return View(await Task.Run(() => hor));
        }
        #endregion

        #region Actualizar

        public async Task<IActionResult> Edit(int idHorarioMedico)
        {
            if (idHorarioMedico == null)
            {
                return RedirectToAction("FilterHorario");
            }

            HorarioMedico hor = await Task.Run(() => _Hor.ListaHorarioMedico().FirstOrDefault(p => p.idHorarioMedico == idHorarioMedico));

            if (hor == null)
            {
                return NotFound();
            }

            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
            ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico"
            );
            return View(hor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HorarioMedico hor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
                ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico"
                );

                return View(hor);
            }

            string mensaje = await Task.Run(() => _Hor.ActualizarHorario(hor));

            ViewBag.mensaje = mensaje;

            if (mensaje.Contains("actualizado"))
            {
                return RedirectToAction("FilterHorario");
            }

            ViewBag.dias = new SelectList(_Dia.ListaDiaSemana(), "idDia", "nombreDia");
            ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico"
            );

            return View(hor);
        }
        #endregion

        #region Eliminar

        public async Task<IActionResult> Delete(int? idHorarioMedico)
        {
            if (idHorarioMedico == null) return RedirectToAction("FilterHorario");

            ViewBag.mensaje = _Hor.EliminarHorario (idHorarioMedico.Value);
            return RedirectToAction("FilterHorario");
        }
        #endregion
    }
}
