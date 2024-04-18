using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Estacionamiento_C_MVC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var vista = View();
            return vista;
        }

        public IActionResult Privacy2()
        {

            //si sucede algo o soy alguien especial
            if (false)
            {
                return View("Test");
            }

            return View();
        }

    }

}
