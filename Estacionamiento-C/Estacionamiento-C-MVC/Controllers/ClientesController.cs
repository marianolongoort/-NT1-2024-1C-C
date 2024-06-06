using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estacionamiento_C_MVC.Data;
using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Estacionamiento_C_MVC.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly MiBaseDeDatos _miDb;
        private readonly UserManager<Persona> _userManager;

        public ClientesController(MiBaseDeDatos context,UserManager<Persona> userManager)
        {
            _miDb = context;
            this._userManager = userManager;
        }

        [Authorize(Roles = $"{Misc.AdministradorRolName},{Misc.EmpleadoRolName}")]
        public IActionResult Index()
        {
            var clientesEnDb = _miDb.Clientes
                                        .OrderBy(clt => clt.Apellido)
                                            .ThenBy(clt => clt.Nombre)
                                        ;
            if (true)
            {
                clientesEnDb.OrderBy(clt => clt.Nombre);
            }

            return View(clientesEnDb);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (User.IsInRole(Misc.ClienteRolName))
            {
                //hace algo
                id = Int32.Parse(_userManager.GetUserId(User));
            }

            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _miDb.Clientes
                                        .Include(clt => clt.Direccion)                                        
                                        .FirstOrDefaultAsync(clt => clt.Id == id);


            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = $"{Misc.AdministradorRolName},{Misc.EmpleadoRolName}")]
        public IActionResult Create()
        {
            



            return View();
        }


        [Authorize(Roles = $"{Misc.AdministradorRolName},{Misc.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CUIL,Nombre,Email,FechaAlta,Apellido,Dni,Id,UserName,NormalizedUserName,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _miDb.Add(cliente);
                await _miDb.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        [Authorize(Roles = $"{Misc.AdministradorRolName},{Misc.EmpleadoRolName}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _miDb.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }


        [Authorize(Roles = $"{Misc.AdministradorRolName},{Misc.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CUIL,Nombre,Email,FechaAlta,Apellido,Dni,Id,UserName,NormalizedUserName,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _miDb.Update(cliente);
                    await _miDb.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        [Authorize(Roles = $"{Misc.AdministradorRolName}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _miDb.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = $"{Misc.AdministradorRolName}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _miDb.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _miDb.Clientes.Remove(cliente);
            }

            await _miDb.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _miDb.Clientes.Any(e => e.Id == id);
        }
    }
}
