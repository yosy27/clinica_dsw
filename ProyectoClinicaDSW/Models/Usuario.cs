using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoClinicaDSW.Models
{
    public class Usuario
    {
        [Required, Display(Name = "Id Usuario")]
        public int? idUsuario { get; set; }

        [Required, Display(Name = "Nombre")]
        public string? Nombre { get; set; } 

        [Required, Display(Name = "Correo")]
        public string? correoUsuario { get; set; }

        [Required, Display(Name = "Contraseña")]
        public string? clave { get; set; }

        [NotMapped]
        public bool MantenerActivo { get; set; }

    }
}
