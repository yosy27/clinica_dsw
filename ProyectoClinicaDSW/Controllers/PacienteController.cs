using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoClinicaDSW.Models;
using ProyectoClinicaDSW.Repositorio;
using ProyectoClinicaDSW.Repositorio.DatosSQL;

namespace ProyectoClinicaDSW.Controllers
{
    public class PacienteController : Controller
    {
        IPaciente _Pac;

        public PacienteController()
        {
            _Pac = new PacienteSQL();
        }

        public async Task<IActionResult> ListadoPaciente()
        {
            return View(await Task.Run(() => _Pac.ListaPacientes()));
        }

        #region Registrar
        public async Task<IActionResult> Create()
        {
            return View(await Task.Run(() => new Paciente()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Paciente pac)
        {
            ViewBag.mensaje = _Pac.RegistrarPaciente(pac);

            return View(await Task.Run(() => pac));
        }
        #endregion

        #region Actualizar
        public async Task<IActionResult> Edit(int idPaciente)
        {
            Paciente paciente = await Task.Run(() => _Pac.ListaPacientes().FirstOrDefault(p => p.idPaciente == idPaciente));

            if (paciente == null)
            {
                return NotFound(); 
            }

            return View(paciente);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Paciente pac)
        {
            if (ModelState.IsValid) 
            {
                ViewBag.mensaje = await Task.Run(() => _Pac.ActualizarPaciente(pac));

                return RedirectToAction("ListadoPaciente"); 
            }

            return View(pac);

        }
        #endregion

        #region Eliminar

        public async Task<IActionResult> Delete(int? idPaciente)
        {
            if (idPaciente == null) return RedirectToAction("ListadoPaciente");

            ViewBag.mensaje = _Pac.EliminarPaciente(idPaciente.Value);
            return RedirectToAction("ListadoPaciente");
        }
        #endregion

        public async Task<IActionResult> FiltroPaciente(string nombre = "")
        {
            return View(await Task.Run(() => _Pac.FilterName(nombre)));
        }

    }
}
