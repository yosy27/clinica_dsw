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

        public async Task<IActionResult> FilterCita(string dni)
        {
            return View(await Task.Run(() => _Cit.FilterCita(dni)));
        }


        #region Registrar
        public async Task<IActionResult> Create()
        {
            ViewBag.pacientes = new SelectList(_Pac.ListaPacientes(), "idPaciente", "nombrePaciente");
            ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico");
            ViewBag.estados = new SelectList(_Est.ListaEstados(), "idEstado", "nombreEstado");
            return View(await Task.Run(() => new Cita()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cita cit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.pacientes = new SelectList(_Pac.ListaPacientes(), "idPaciente", "nombrePaciente");
                ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico");
                ViewBag.estados = new SelectList(_Est.ListaEstados(), "idEstado", "nombreEstado");
            }
            ViewBag.mensaje = _Cit.RegistrarCita(cit);

            ViewBag.pacientes = new SelectList(_Pac.ListaPacientes(), "idPaciente", "nombrePaciente");
            ViewBag.medicos = new SelectList(_Med.ListaMedico(), "idMedico", "nombreMedico");
            ViewBag.estados = new SelectList(_Est.ListaEstados(), "idEstado", "nombreEstado");
            return View(await Task.Run(() => cit));
        }
        #endregion

    }
}
