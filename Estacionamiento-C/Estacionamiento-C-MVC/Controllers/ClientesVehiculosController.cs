using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estacionamiento_C_MVC.Data;
using Estacionamiento_C_MVC.Models;

namespace Estacionamiento_C_MVC.Controllers
{
    public class ClientesVehiculosController : Controller
    {
        private readonly MiBaseDeDatos _miBaseDeDatos;

        public ClientesVehiculosController(MiBaseDeDatos context)
        {
            _miBaseDeDatos = context;
        }

        // GET: ClientesVehiculos
        public async Task<IActionResult> Index()
        {
            var miBaseDeDatos = _miBaseDeDatos.ClientesVehiculos
                                                .Include(c => c.Cliente)
                                                .Include(c => c.Vehiculo);


            return View(await miBaseDeDatos.ToListAsync());
        }

        // GET: ClientesVehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _miBaseDeDatos.ClientesVehiculos
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            return View(clienteVehiculo);
        }

        // GET: ClientesVehiculos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_miBaseDeDatos.Clientes, "Id", "NombreCompleto");
            ViewData["VehiculoId"] = new SelectList(_miBaseDeDatos.Vehiculos, "Id", "Patente");
         

            return View();
        }

        // POST: ClientesVehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,VehiculoId")] ClienteVehiculo clienteVehiculo)
        {
            if (ModelState.IsValid)
            {
                _miBaseDeDatos.Add(clienteVehiculo);
                await _miBaseDeDatos.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_miBaseDeDatos.Clientes, "Id", "NombreCompleto", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_miBaseDeDatos.Vehiculos, "Id", "Patente", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // GET: ClientesVehiculos/Edit/5
        public async Task<IActionResult> Edit(int? clienteId,int? vehiculoId)
        {
            if (clienteId == null || vehiculoId == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _miBaseDeDatos.ClientesVehiculos.FindAsync(clienteId,vehiculoId);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_miBaseDeDatos.Clientes, "Id", "NombreCompleto", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_miBaseDeDatos.Vehiculos, "Id", "Patente", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // POST: ClientesVehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? clienteid,int? vehiculoid, [Bind("ClienteId,VehiculoId")] ClienteVehiculo clienteVehiculo)
        {
            //if (clienteId != clienteVehiculo.ClienteId || vehiculoId != clienteVehiculo.VehiculoId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _miBaseDeDatos.Update(clienteVehiculo);
                    await _miBaseDeDatos.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteVehiculoExists(clienteVehiculo.ClienteId))
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
            ViewData["ClienteId"] = new SelectList(_miBaseDeDatos.Clientes, "Id", "Discriminator", clienteVehiculo.ClienteId);
            ViewData["VehiculoId"] = new SelectList(_miBaseDeDatos.Set<Vehiculo>(), "Id", "Id", clienteVehiculo.VehiculoId);
            return View(clienteVehiculo);
        }

        // GET: ClientesVehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteVehiculo = await _miBaseDeDatos.ClientesVehiculos
                .Include(c => c.Cliente)
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (clienteVehiculo == null)
            {
                return NotFound();
            }

            return View(clienteVehiculo);
        }

        // POST: ClientesVehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteVehiculo = await _miBaseDeDatos.ClientesVehiculos.FindAsync(id);
            if (clienteVehiculo != null)
            {
                _miBaseDeDatos.ClientesVehiculos.Remove(clienteVehiculo);
            }

            await _miBaseDeDatos.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteVehiculoExists(int id)
        {
            return _miBaseDeDatos.ClientesVehiculos.Any(e => e.ClienteId == id);
        }
    }
}
