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

        public async Task<IActionResult> Seed()
        {
            //Inicializar DB
            InicializarDB();


            //crear roles
            CrearRoles();

            //Clientes

            //Direcciones
            //Telefonos

            return RedirectToAction("Index","Home",new { mensaje = "DB precargada"});
        }

        private void InicializarDB()
        {
            _midb.Database.EnsureDeleted();
            _midb.Database.Migrate();
        }

        private async Task CrearRoles()
        {
            Rol roladmin = new Rol("Administrador");
            Rol rolclt = new Rol("Cliente");
            Rol rolemp = new Rol("Empleado");

            List<Rol> roles = new List<Rol>();
            roles.Add(new Rol("Administrador"));
            roles.Add(new Rol("Cliente"));
            roles.Add(new Rol("Empleado"));

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }

            await _roleManager.CreateAsync(new Rol("Administrador"));
            await _roleManager.CreateAsync(new Rol("Cliente"));
            await _roleManager.CreateAsync(new Rol("Empleado"));


        }
    }
}
