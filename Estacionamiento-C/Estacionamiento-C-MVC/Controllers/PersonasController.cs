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
    public class PersonasController : Controller
    {
        private readonly MiBaseDeDatos _miDb;

        public PersonasController(MiBaseDeDatos context)
        {
            _miDb = context;
        }

        // GET: Personas
        public IActionResult Index()
        {
            return View(_miDb.Personas.ToList());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _miDb.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }





        //Ofrecer el formulario de creación de personas    
        public IActionResult Create()
        {
            Persona persona = new Persona() { 
                Nombre = "Mariano",
                Apellido = "Longo",
                Email = "mlongo@ort.edu.ar",
                Dni = 22333444
            };            

            return View(persona);
        }

        

        //Procesar la información recibida del cliente
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Persona persona)            //[Bind("Nombre,Email,Fecha,Hora,FechaAlta,Apellido,Dni,Id,UserName,NormalizedUserName,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] 
        {
            if (ModelState.IsValid)
            {
                _miDb.Add(persona);
                _miDb.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }












    //oferta de formulario
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _miDb.Personas.Find(id); 
            

            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        //procesar la info
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _miDb.Update(persona);
                    await _miDb.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _miDb.Personas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _miDb.Personas.FindAsync(id);
            if (persona != null)
            {
                _miDb.Personas.Remove(persona);
            }

            await _miDb.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _miDb.Personas.Any(e => e.Id == id);
        }
    }
}
