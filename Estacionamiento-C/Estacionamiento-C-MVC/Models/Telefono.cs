namespace Estacionamiento_C_MVC.Models
{
    public class Telefono
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        //Prop NAv
        public Cliente Cliente { get; set; }

        //prop  relacional
        public int ClienteId { get; set; }
    }
}
