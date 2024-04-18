using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estacionamiento_C_MVC.Data
{
    public class MiBaseDeDatos : IdentityDbContext
    {
        public MiBaseDeDatos(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //especializar nosotros

            //definir cosas para las relaciones muchos a muchos.

        }



        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }


    }
}
