namespace ProyectoClinicaDSW.Models
{
    public class Horario
    {
        public int idHorario { get; set; }
        public int IdDia { get; set; }
        public TimeSpan horaInicio { get; set; } 
        public TimeSpan horaFin { get; set; }

    }
}
