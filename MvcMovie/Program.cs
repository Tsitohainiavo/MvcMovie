using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajout du contexte de base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configuration d'ASP.NET Core Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>() // Utilisez IdentityUser et IdentityRole
    .AddEntityFrameworkStores<ApplicationDbContext>() // Liez Identity à votre DbContext
    .AddDefaultTokenProviders(); // Ajoutez des fournisseurs de jetons par défaut

// Ajout des services de contrôleurs et de vues
builder.Services.AddControllersWithViews();

//Configuration de la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de vie de la session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession(); // Place cette ligne après app.UseRouting() et avant app.UseAuthorization()

// Activation d'Identity (Authentification et Autorisation)
app.UseAuthentication(); // <-- Ajoutez cette ligne pour activer l'authentification
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Utilisateur}/{action=Login}/{id?}");

app.Run();