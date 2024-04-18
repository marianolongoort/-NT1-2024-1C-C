using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamiento_C_MVC.Controllers
{
    public class Personas2Controller : Controller
    {
        public IActionResult Index(string nombre,string apellido)
        {
            Persona persona = new Persona();
            persona.Nombre = nombre;
            persona.Apellido = apellido;

            return View(persona);
        }



    }
}
