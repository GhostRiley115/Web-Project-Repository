using Microsoft.AspNetCore.Authentication.Cookies;
using ProjetoWeb1.Interfaces;
using ProjetoWeb1.Repositorios;

var builder = WebApplication.CreateBuilder(args);

//Configurar o serviço de autenticação do sistema

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options =>
{
    Options.LoginPath = "/Usuario/Logar";
    Options.AccessDeniedPath = "/Usuario/AcessogoNegado";
});

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
