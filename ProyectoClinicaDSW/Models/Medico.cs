using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class Medico
    {
        [Required, Display(Name = "Id Médico")]
        public int? idMedico { get; set; }

        [Display(Name = "Nombres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string? nombreMedico { get; set; }

        [Display(Name = "Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string? apellidoMedico { get; set; }

        [Display(Name = "DNI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string? dni { get; set; }

        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "Debe seleccionar una especialidad.")]
        public int? idEspecialidad { get; set; }

        [Display(Name = "Contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string? contacto { get; set; }

        [Display(Name = "Horario de Atención")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string? horario { get; set; }

    }
}
