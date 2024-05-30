using System.ComponentModel.DataAnnotations;

namespace Estacionamiento_C_MVC.Models
{
    public class RegistrarVM
    {

        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [EmailAddress(ErrorMessage = "Debe ser un correo")]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Escribí bien")]
        public string Confirmacion { get; set; }

    }
}
