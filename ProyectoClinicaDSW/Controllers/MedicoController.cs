using Microsoft.AspNetCore.Mvc;
using ProyectoClinicaDSW.Repositorio.DatosSQL;
using ProyectoClinicaDSW.Repositorio;
using ProyectoClinicaDSW.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoClinicaDSW.Controllers
{
    public class MedicoController : Controller
    {
        IMedico _Med;
        IEspecialidad _Esp;

        public MedicoController()
        {
            _Med = new MedicoSQL();
            _Esp = new EspecialidadSQL();

        }
        public async Task<IActionResult> FilterMedico(string inicial)
        {
            return View(await Task.Run(() => _Med.FilterMedico(inicial)));
        }

        #region Registrar
        public async Task<IActionResult> Create()
        {
            ViewBag.especialidades = new SelectList(_Esp.ListaEspecialidades(), "idEspecialidad", "nombreEspecialidad");
            return View(await Task.Run(() => new Medico()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Medico med)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.especialidades = new SelectList(_Esp.ListaEspecialidades(), "idEspecialidad", "nombreEspecialidad", med.idEspecialidad);
            }
            ViewBag.mensaje = _Med.RegistrarMedico(med);

            ViewBag.especialidades = new SelectList(_Esp.ListaEspecialidades
                (), "idEspecialidad", "nombreEspecialidad", med.idEspecialidad);
            return View(await Task.Run(() => med));
        }
        #endregion

        #region Actualizar

        public async Task<IActionResult> Edit(int? idMedico = null)
        {
            if (idMedico == null)
            {
                return RedirectToAction("FilterMedico");
            }

            Medico med = await Task.Run(() => _Med.ListaMedico().FirstOrDefault(p => p.idMedico == idMedico));

            if (med == null)
            {
                return NotFound();
            }

            ViewBag.especialidades = new SelectList(
                _Esp.ListaEspecialidades(),
                "idEspecialidad",
                "nombreEspecialidad",
                med.idEspecialidad
            );
            return View(med);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Medico med)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.especialidades = new SelectList(
                    _Esp.ListaEspecialidades(),
                    "idEspecialidad",
                    "nombreEspecialidad",
                    med.idEspecialidad
                );

                return View(med);
            }

            string mensaje = await Task.Run(() => _Med.ActualizarMedico(med));

            ViewBag.mensaje = mensaje;

            if (mensaje.Contains("actualizado"))
            {
                return RedirectToAction("FilterMedico");
            }

            ViewBag.especialidades = new SelectList(
                _Esp.ListaEspecialidades(),
                "idEspecialidad",
                "nombreEspecialidad",
                med.idEspecialidad
            );

            return View(med);
        }
        #endregion

        #region Eliminar

        public async Task<IActionResult> Delete(int? idMedico)
        {
            if (idMedico == null) return RedirectToAction("FilterMedico");

            ViewBag.mensaje = _Med.EliminarMedico(idMedico.Value);
            return RedirectToAction("FilterMedico");
        }
        #endregion


    }
}
