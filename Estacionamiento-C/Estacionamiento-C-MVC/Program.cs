using Estacionamiento_C_MVC.Data;
using Estacionamiento_C_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Estacionamiento_C_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<MiBaseDeDatos>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EstacionamientoCS")));

            builder.Services.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<MiBaseDeDatos>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5; //por defecto 6
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
                {
                    //opciones.LoginPath = "/Account/TBD";
                    //opciones.AccessDeniedPath = "/TBD";
                    opciones.Cookie.Name = "GarageApp";
                });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();







            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
