using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estacionamiento_C_MVC.Models
{
    public class Persona : IdentityUser<int>
    {
        //sin restricciones por ahora
        //public int Id { get; set; }


        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [StringLength(25,MinimumLength = 5,ErrorMessage = "{0} debe ser entre {2} y {1}")]
        public string Nombre { get; set; }



        [Required(ErrorMessage = "La propiedad {0} es requerida")]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }


        //[DataType(DataType.Password)]
        //public string Password { get; set; }


        public DateTime FechaAlta { get; set; } = DateTime.Now;


        [MinLength(5)]
        [MaxLength(25)]
        public string Apellido { get; set; }


        [Required]
        [Range(1000,99999999,ErrorMessage = "{0} debe estar entre {1} y {2}")]
        public int Dni { get; set; }

        //sin restricciones por ahora
        public Direccion Direccion { get; set; }

        [NotMapped]
        public string NombreCompleto 
        {
            get { 
                return $"{Apellido}, {Nombre}";
            }
        }
    }
}
