using System.Data;
using AngularCRUDvs.Entidades;
using AngularCRUDvs.Models;
using AngularCRUDvs.Services.Contratos;
using AngularCRUDvs.Services.Implementaciones;
using AngularCRUDvs.Util;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML.Voice;

namespace AngularCRUDvs.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    Seguridad Seguridad = new Seguridad();
    private IConfiguration _config;
    private IMapper _mapper;
    private IPersonaServices _personaServices;
    private IClaimsServices _claimsServices;
    private IUnidadServices _unidadServices;
    private IReciboServices _reciboServices;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IMapper mapper, IPersonaServices personaServices, IClaimsServices claimsServices, IUnidadServices unidadServices, IReciboServices reciboServices)
    {
        _logger = logger;
        _config = config;
        _mapper = mapper;
        _personaServices = personaServices;
        _claimsServices = claimsServices;
        _unidadServices = unidadServices;
        _reciboServices = reciboServices;

    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

