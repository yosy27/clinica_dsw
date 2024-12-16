using Microsoft.AspNetCore.Authentication.Cookies;
using ProyectoClinicaDSW.Data;
using ProyectoClinicaDSW.Repositorio.DatosSQL;
using ProyectoClinicaDSW.Repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(new Contexto(builder.Configuration.GetConnectionString("cnx")));


builder.Services.AddScoped<IUsuario, UsuarioSQL>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
