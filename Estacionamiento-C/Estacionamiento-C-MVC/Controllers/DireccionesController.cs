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
    public class DireccionesController : Controller
    {
        private readonly MiBaseDeDatos _miDb;

        public DireccionesController(MiBaseDeDatos db)
        {
            _miDb = db;
        }

        
        public IActionResult Index()
        {
            var direccionesEnDb = _miDb.Direcciones
                                        .Include(direccion => direccion.Persona)
                                        .ToList()
                                        ;
            
            
            return View( direccionesEnDb);
        }

        // GET: Direcciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _miDb.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // GET: Direcciones/Create
        public IActionResult Create(bool precarga = false,bool condireccion = false)
        {

            var personas = _miDb.Personas.Include(p => p.Direccion);
            
            IQueryable personasEnDb;

            if (condireccion)
            {
                personasEnDb = personas;
            }
            else
            {
                personasEnDb = personas.Where(p => p.Direccion == null);
            }

            ViewData["PersonaId"] = new SelectList(personasEnDb, "Id", "NombreCompleto");

            if (precarga)
            {
                Direccion dir = new Direccion() { Calle = "Corrientes", Numero = 2222 };
                return View(dir);
            }
            
            return View();
        }

        // POST: Direcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Calle,Numero,PersonaId")] Direccion direccion)
        {
            //ModelState.Remove("Calle");

            if (ModelState.IsValid)
            {
                _miDb.Add(direccion);
                await _miDb.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["PersonaId"] = new SelectList(_miDb.Personas, "Id", "NombreCompleto", direccion.PersonaId);
            
            return View(direccion);
        }

        // GET: Direcciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _miDb.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_miDb.Personas, "Id", "NombreCompleto", direccion.PersonaId);
            return View(direccion);
        }

        // POST: Direcciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Calle,Numero,PersonaId")] Direccion direccion)
        {
            if (id != direccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _miDb.Update(direccion);
                    await _miDb.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.Id))
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
            ViewData["PersonaId"] = new SelectList(_miDb.Personas, "Id", "NombreCompleto", direccion.PersonaId);
            return View(direccion);
        }

        // GET: Direcciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _miDb.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // POST: Direcciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccion = await _miDb.Direcciones.FindAsync(id);
            if (direccion != null)
            {
                _miDb.Direcciones.Remove(direccion);
            }

            await _miDb.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
            return _miDb.Direcciones.Any(e => e.Id == id);
        }
    }
}
