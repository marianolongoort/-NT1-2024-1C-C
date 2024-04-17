namespace Estacionamiento_C_MVC.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Patente { get; set; }



        public List<Cliente> Clientes { get; set; }

        public List<ClienteVehiculo> ClientesVehiculos { get; set; }

    }
}
