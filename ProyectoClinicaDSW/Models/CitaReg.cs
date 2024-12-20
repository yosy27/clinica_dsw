using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class CitaReg
    {

        [Required, Display(Name = "Id Cita")]
        public int? idCita { get; set; }

        [Required, Display(Name = "Descripción")]
        public string nombreCita { get; set; }

        [Required, Display(Name = "Paciente")]
        public int? idPaciente { get; set; }


        [Required, Display(Name = "Médico")]
        public int? idMedico { get; set; }

        [Required, Display(Name = "Fecha y Hora de la Cita ")]
        public DateTime? fechaHora { get; set; }

        [Required, Display(Name = "Estado")]
        public int? idEstado { get; set; }

    }
}
