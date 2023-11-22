using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularCRUDvs.Models;
using AngularCRUDvs.Data;

namespace AngularCRUDvs.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Personas")]
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        public IEnumerable<PersonaModel> GetPersonas()
        {
            return (IEnumerable<PersonaModel>)_context.Persona;
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersona([FromRoute] int id, bool incluirDirecciones = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaModel persona = new PersonaModel();

            if (incluirDirecciones)
            {
                //persona = await _context.Personas.Include(x => x.Direcciones).SingleOrDefaultAsync(m => m.Id == id);
            }
            else
            {
                //persona = await _context.Personas.SingleOrDefaultAsync(m => m.Id == id);
            }

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        private async Task CrearOEditarDirecciones(List<DireccionModel> direcciones)
        {
            List<DireccionModel> direccionesACrear = direcciones.Where(x => x.Id == 0).ToList();
            List<DireccionModel> direccionesAEditar = direcciones.Where(x => x.Id != 0).ToList();

            if (direccionesACrear.Any())
            {
                await _context.AddRangeAsync(direccionesACrear);
            }

            if (direccionesAEditar.Any())
            {
                _context.UpdateRange(direccionesAEditar);
            }

        }

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona([FromRoute] int id, [FromBody] PersonaModel persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            _context.Entry(persona).State = EntityState.Modified;

            try
            {
                await CrearOEditarDirecciones(persona.Direcciones);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Personas
        [HttpPost]
        public async Task<IActionResult> PostPersona([FromBody] PersonaModel persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //_context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = await _context.Persona.SingleOrDefaultAsync(m => m.PersonaId == id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok(persona);
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.PersonaId == id);
        }
    }
}