using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class Cita
    {
        [Required, Display(Name = "Id Cita")]
        public int? idCita { get; set; }

        [Required, Display(Name = "Id Paciente")]
        public int? idPaciente { get; set; }

        [Required, Display(Name = "Id Médico")]
        public int? idMedico { get; set; }

        [Required, Display(Name = "Fecha y Hora")]
        public DateTime? fechaHora { get; set; }

        [Required, Display(Name = "Id Estado")]
        public int? idEstado { get; set; }

    }
}
