using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estacionamiento_C_MVC.Data
{
    public class MiBaseDeDatos : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public MiBaseDeDatos(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //especializar nosotros
            //modelBuilder.Entity<Direccion>().HasKey(direccion => direccion.Id);


            //definir cosas para las relaciones muchos a muchos.
            modelBuilder.Entity<ClienteVehiculo>().HasKey(cv => new { cv.ClienteId,cv.VehiculoId});

            modelBuilder.Entity<ClienteVehiculo>().HasOne(cv => cv.Cliente)
                                                    .WithMany(cliente => cliente.ClientesVehiculos)
                                                        .HasForeignKey(cv => cv.ClienteId);

            modelBuilder.Entity<ClienteVehiculo>().HasOne(cv => cv.Vehiculo)
                                                    .WithMany(vehiculo => vehiculo.ClientesVehiculos)
                                                        .HasForeignKey(cv => cv.VehiculoId);

            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas");
            //modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");

        }



        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<ClienteVehiculo> ClientesVehiculos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Rol> MisRoles { get; set; }

    }
}
