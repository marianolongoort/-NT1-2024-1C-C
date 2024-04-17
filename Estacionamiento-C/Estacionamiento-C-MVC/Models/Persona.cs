using System.ComponentModel.DataAnnotations;

namespace Estacionamiento_C_MVC.Models
{
    public class Persona
    {
        //sin restricciones por ahora
        public int Id { get; set; }

        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [StringLength(25,MinimumLength = 5,ErrorMessage = "{0} debe ser entre {2} y {1}")]
        public string Nombre { get; set; }



        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Date)]
        public DateOnly Fecha { get; set; }


        [DataType(DataType.Time)]
        public TimeOnly Hora { get; set; }


        public DateTime FechaAlta { get; set; }


        [MinLength(5)]
        [MaxLength(25)]
        public string Apellido { get; set; }


        [Required]
        [Range(1000,99999999,ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public int Dni { get; set; }

        //sin restricciones por ahora
        public Direccion Direccion { get; set; }


    }
}
