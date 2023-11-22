﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngularCRUDvs.Models;
using AngularCRUDvs.Data;

namespace AngularCRUDvs.Controllers
{
    [Produces("application/json")]
    [Route("api/Direcciones")]
    public class DireccionesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public DireccionesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("delete/list")]
        public IActionResult DeleteList([FromBody] List<int> ids)
        {
            try
            {
                List<DireccionModel> direcciones = ids.Select(id => new DireccionModel() { Id = id }).ToList();
                dbContext.RemoveRange(direcciones);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

    }
}