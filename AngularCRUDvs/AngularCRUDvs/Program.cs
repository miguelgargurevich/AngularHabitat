//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action=Index}/{id?}");

//app.MapFallbackToFile("index.html");

//app.Run();



//using AngularCRUDvs.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using AngularCRUDvs.Data;
using AngularCRUDvs.Services.Contratos;
using AngularCRUDvs.Services.Implementaciones;
using AngularCRUDvs.Repository.Implementaciones;
using AngularCRUDvs.Repository.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AngularCRUDvs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var configuration = new ConfigurationBuilder();
var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();


//// Add DbContext to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.EnableSensitiveDataLogging(); // Opcional, útil para la depuración
}, ServiceLifetime.Singleton);



// Start Registering and Initializing AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure DI for application services
builder.Services.AddSingleton<IEmailServices, EmailServices>();
builder.Services.AddSingleton<IClaimsServices, ClaimsServices>();

builder.Services.AddSingleton<IPersonaServices, PersonaServices>();
builder.Services.AddSingleton<IConceptoServices, ConceptoServices>();
builder.Services.AddSingleton<IReciboServices, ReciboServices>();
builder.Services.AddSingleton<IReciboPagoServices, ReciboPagoServices>();
builder.Services.AddSingleton<IReciboConceptoServices, ReciboConceptoServices>();
builder.Services.AddSingleton<IUnidadServices, UnidadServices>();

builder.Services.AddSingleton<IPersonaRepository, PersonaRepository>();
builder.Services.AddSingleton<IConceptoRepository, ConceptoRepository>();
builder.Services.AddSingleton<IReciboRepository, ReciboRepository>();
builder.Services.AddSingleton<IReciboPagoRepository, ReciboPagoRepository>();
builder.Services.AddSingleton<IReciboConceptoRepository, ReciboConceptoRepository>();
builder.Services.AddSingleton<IUnidadRepository, UnidadRepository>();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
//{
//    option.LoginPath = "/Account/Login";
//    option.Cookie.Name = "my_app_auth_cookie";
//});

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//              .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = "yourdomain.com",
//         ValidAudience = "yourdomain.com",
//         IssuerSigningKey = new SymmetricSecurityKey(
//        Encoding.UTF8.GetBytes("Llave_super_secreta")),
//         ClockSkew = TimeSpan.Zero
//     });


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
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
