using Estacionamiento_C_MVC.Data;
using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamiento_C_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly MiBaseDeDatos _midb;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly SignInManager<Persona> _signInManager;

        public AccountController(
            MiBaseDeDatos midb,
            UserManager<Persona> userManager,
            RoleManager<Rol> roleManager,
            SignInManager<Persona> signInManager            
            )
        {
            this._midb = midb;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
        }

        #region Registración cliente
        //Oferta de formulario de registración
        public IActionResult Registrar()
        {
            return View();
        }

        //Procesar la info de registración del cliente
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarVM datosFormulario)
        {
            if (ModelState.IsValid)
            {
                //agregamos a la base
                //_midb.Clientes.Add(clienteDeFormulario);
                //_midb.SaveChanges();
                Cliente cliente = new Cliente() { 
                    Email = datosFormulario.Email,
                    UserName = datosFormulario.Email
                };

                var resultCreate = await _userManager.CreateAsync(cliente,datosFormulario.Password);

                if (resultCreate.Succeeded)
                {
                    //avanzamos con lo que sea necesario
                    //agregamos un rol

                    //Iniciar sesión en cliente
                    await _signInManager.SignInAsync(cliente,false);

                    return RedirectToAction("Index","Home");
                }
                //tratamiento del error si fuese necesario                
            }
            //si tengo que tratar el error de alguna manera.

            return View(datosFormulario);
        }

        #endregion

        #region Iniciar Cerrar
        public IActionResult IniciarSesion()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Clientes");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                //seguimos
                var resultado = await _signInManager.PasswordSignInAsync(model.Email,model.Password,model.Recordarme,false);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index","Clientes");
                }

                ModelState.AddModelError(string.Empty,"Algo salió mal, intentá de nuevo");
                //ModelState.AddModelError("Email","Este es un error de correo");
            }
            //vemos que hacemos

            return View(model);
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("IniciarSesion", "Account");
        }
        #endregion
    }
}
