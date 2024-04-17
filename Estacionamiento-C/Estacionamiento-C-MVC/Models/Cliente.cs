namespace Estacionamiento_C_MVC.Models
{
    public class Cliente : Persona
    {
        public long CUIL { get; set; }

        
        //Prop Nav
        public List<Telefono> Telefonos { get; set; }

        public List<ClienteVehiculo> ClientesVehiculos { get; set; }
    }
}
