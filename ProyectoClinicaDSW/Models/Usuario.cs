using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class Usuario
    {
        [Required, Display(Name = "Id Usuario")]
        public int? idUsuario { get; set; }

        [Required, Display(Name = "Nombre")]
        public string? nombreUsuario { get; set; }

        [Required, Display(Name = "Correo")]
        public string? correoUsuario { get; set; }

        [Required, Display(Name = "Contraseña")]
        public string? clave { get; set; }

        [Required, Display(Name = "Rol")]
        public int? idRol { get; set; }

        
    }
}
