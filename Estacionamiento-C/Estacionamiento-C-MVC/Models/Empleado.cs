using System.ComponentModel.DataAnnotations;

namespace Estacionamiento_C_MVC.Models
{
    public class Empleado: Persona
    {
        [Required]
        [StringLength(50)]
        public string Legajo { get; set; }
    }
}
