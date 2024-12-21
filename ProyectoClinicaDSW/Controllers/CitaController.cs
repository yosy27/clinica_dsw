using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoClinicaDSW.Models;
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

        public async Task<IActionResult> FilterCita(string nombre)
        {
            return View (await Task.Run(() => _Cit.FilterCita(nombre)));
        }


        #region Registrar
        public async Task<IActionResult> Create()
        {
            ViewBag.pacientes = new SelectList(_Pac.ListaPacientes(), "idPaciente", "nombrePaciente");
            ViewBag.medicos = new SelectList(_Med.ListMedicoCb(), "idMedico", "nombreMedico");
            ViewBag.estados = new SelectList(_Est.ListaEstados(), "idEstado", "nombreEstado");
            return View(await Task.Run(() => new CitaReg()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CitaReg citReg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.pacientes = new SelectList(_Pac.ListaPacientes(), "idPaciente", "nombrePaciente");
                ViewBag.medicos = new SelectList(_Med.ListMedicoCb(), "idMedico", "nombreMedico");
                ViewBag.estados = new SelectList(_Est.ListaEstados(), "idEstado", "nombreEstado");
            }
            ViewBag.mensaje = _Cit.RegistrarCita(citReg);

            ViewBag.pacientes = new SelectList(_Pac.ListaPacientes(), "idPaciente", "nombrePaciente");
            ViewBag.medicos = new SelectList(_Med.ListMedicoCb(), "idMedico", "nombreMedico");
            ViewBag.estados = new SelectList(_Est.ListaEstados(), "idEstado", "nombreEstado");
            return View(await Task.Run(() => citReg));
        }
        #endregion

        #region Eliminar

        public async Task<IActionResult> Delete(int? idCita)
        {
            if (idCita == null) return RedirectToAction("FilterCita");

            ViewBag.mensaje = _Med.EliminarMedico(idCita.Value);
            return RedirectToAction("FilterCita");
        }
        #endregion

    }
}
