using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class Paciente
    {
        [Required, Display(Name = "Id Paciente")]
        public int? idPaciente { get; set; }

        [Required, Display(Name = "Nombres")]
        public string? nombrePaciente { get; set; }

        [Required, Display(Name = "Apellidos")]
        public string? apellidoPaciente { get; set; }

        [Required, Display(Name = "Correo")]
        public string? correoPaciente { get; set; }

        [Required, Display(Name = "Telefono")]
        public string? telefonoPaciente { get; set; }

        [Required, Display(Name = "DNI")]
        public string? dni { get; set; }

        [Required, Display(Name = "Fecha de Registro")]
        public DateTime? fechaRegistro { get; set; }

    }
}
