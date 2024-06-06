using Estacionamiento_C_MVC.Data;
using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estacionamiento_C_MVC.Controllers
{
    public class PreCargaController : Controller
    {
        private readonly MiBaseDeDatos _midb;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;

        public PreCargaController(
            MiBaseDeDatos midb,
            UserManager<Persona> userManager,
            RoleManager<Rol> roleManager
            )
        {
            this._midb = midb;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public IActionResult RecreateDB() {
            RegenerarDb();
            return RedirectToAction("Index", "Home", new { mensaje = "DB regenerada" });
        }

        public IActionResult Seed()
        {
            //Inicializar DB
            RegenerarDb();


            //crear roles
            CrearRoles().Wait();

            //Clientes
            CrearClientes().Wait();

            //Direcciones
            CrearDirecciones();

            //Telefonos

            CrearVehiculos();

            CrearClientesVehiculos();

            CrearEmpleado().Wait();

            return RedirectToAction("Index","Home",new { mensaje = "DB precargada"});
        }

        private async Task CrearEmpleado()
        {
            string email = "empleado1@ort.edu.ar";
            Empleado empleado = new Empleado()
            {
                Legajo = "A123445",
                Nombre = "Pablo",
                Apellido = "Marmol",
                Email = email,
                FechaAlta = DateTime.Now,
                Dni = 55444333,
                UserName = email
            };

            var resultado1 = await _userManager.CreateAsync(empleado, Misc.DefaultPassword);
            if (resultado1.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado, Misc.EmpleadoRolName);
            }
        }

        private void CrearClientesVehiculos()
        {
            _midb.ClientesVehiculos.Add(
                new ClienteVehiculo() { 
                    ClienteId= 1,
                    VehiculoId= 1
                }
                );
            _midb.SaveChanges();
        }

        private void CrearVehiculos()
        {
            Vehiculo vehiculo = new Vehiculo() { 
                Patente = "IIM321"                
            };
            _midb.Vehiculos.Add( vehiculo );
            _midb.SaveChanges();
        }

        private void CrearDirecciones()
        {
            Direccion dir1 = new Direccion() { 
                 Calle = "Cordoba",
                 Numero = 2233,
                 PersonaId = 1            
            };

            _midb.Direcciones.Add(dir1);
            _midb.SaveChanges();

        }

        private async Task CrearClientes()
        {
            string email = "cliente1@ort.edu.ar";
            Cliente cliente = new Cliente()
            {
                CUIL = 20223334440,
                Nombre = "Pedro",
                Apellido = "Picapiedra",
                Email = email,
                FechaAlta = DateTime.Now,
                Dni = 22333444,            
                UserName = email                
            };

            var resultado1 = await _userManager.CreateAsync(cliente,Misc.DefaultPassword);
            if (resultado1.Succeeded)
            {
                await _userManager.AddToRoleAsync(cliente,Misc.ClienteRolName);
            }
        }

        private void RegenerarDb()
        {
            _midb.Database.EnsureDeleted();
            _midb.Database.Migrate();
        }

        private async Task CrearRoles()
        {
            //Rol roladmin = new Rol("Administrador");
            //Rol rolclt = new Rol("Cliente");
            //Rol rolemp = new Rol("Empleado");

            //List<Rol> roles = new List<Rol>();
            //roles.Add(new Rol("Administrador"));
            //roles.Add(new Rol("Cliente"));
            //roles.Add(new Rol("Empleado"));

            //foreach (var role in roles)
            //{
            //    await _roleManager.CreateAsync(role);
            //}

            await _roleManager.CreateAsync(new Rol(Misc.AdministradorRolName));
            await _roleManager.CreateAsync(new Rol(Misc.EmpleadoRolName));
            await _roleManager.CreateAsync(new Rol(Misc.ClienteRolName));


        }
    }
}
