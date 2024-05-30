using System.ComponentModel.DataAnnotations;

namespace Estacionamiento_C_MVC.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [EmailAddress(ErrorMessage = "Debe ser un correo")]
        [Display(Name = "Correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Recordarme { get; set; }
    }
}
