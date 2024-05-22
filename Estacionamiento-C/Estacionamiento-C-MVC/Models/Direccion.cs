using System.ComponentModel.DataAnnotations;

namespace Estacionamiento_C_MVC.Models
{
    public class Direccion
    {        
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =4)]
        public string Calle { get; set; }


        [Range(0,999999)]
        public int Numero { get; set; }

        //Prop Navegacional
        public Persona Persona { get; set; }

        //Prop Relacional
        [Display(Name ="Dueño")]
        public int PersonaId { get; set; }

    }
}
