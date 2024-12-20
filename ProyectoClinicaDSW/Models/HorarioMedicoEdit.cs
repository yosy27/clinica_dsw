using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class HorarioMedicoEdit
    {
        [Required, Display(Name = "Id Horario")]
        public int idHorarioMedico { get; set; }

        [Required, Display(Name = "Hora Inicio")]
        public TimeSpan horaInicio { get; set; }

        [Required, Display(Name = "Hora Fin")]
        public TimeSpan horaFin { get; set; }

        [Required, Display(Name = "Día")]
        public int idDia { get; set; }

        [Required, Display(Name = "Médico")]
        public int idMedico { get; set; }


    }
}
