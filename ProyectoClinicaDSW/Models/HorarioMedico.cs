using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class HorarioMedico
    {
        [Required, Display(Name = "Id Horario Médico")]
        public int idHorarioMedico { get; set; }

        [Required, Display(Name = "Hora Inicio")]
        public TimeSpan horaInicio { get; set; }

        [Required, Display(Name = "Hora Fin")]
        public TimeSpan horaFin { get; set; }

        [Required, Display(Name = "Id Día")]
        public int idDia { get; set; }

        [Required, Display(Name = "Id Médico")]
        public int idMedico { get; set; }
    }
}
