﻿using System.ComponentModel.DataAnnotations;

namespace ProyectoClinicaDSW.Models
{
    public class MedicoEdit
    {
        [Required, Display(Name = "Id Médico")]
        public int idMedico { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string nombreMedico { get; set; }

        [Display(Name = "DNI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo requerido")]
        public string dni { get; set; }

        [Display(Name = "Contacto")]
        public string contacto { get; set; }

        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "Debe seleccionar una especialidad.")]
        public int? idEspecialidad { get; set; }
    }
}
