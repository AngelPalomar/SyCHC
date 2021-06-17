using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorController : ControllerBase
    {
        private readonly AppDbContext context;
        public ConsultorController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ConsultorController>
        [HttpGet]
        public IEnumerable<Consultor> Get()
        {
            return context.Consultor.ToList();
        }

        // GET api/<ConsultorController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var consultor = context.Consultor.FirstOrDefault(cons => cons.Id == id);
            if (consultor != null)
            {
                return Ok(consultor);
            } else
            {
                return NotFound();
            }
        }

        // POST api/<ConsultorController>
        [HttpPost]
        public ActionResult Post([FromBody] Consultor consultor)
        {
            try
            {
                context.Consultor.Add(consultor);
                context.SaveChanges();

                return Ok(consultor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ConsultorController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Consultor consultor)
        {
            var consultorRegistro = context.Consultor.Find(id);
            if (consultorRegistro != null)
            {
                try
                {
                    consultorRegistro.Nombres = consultor.Nombres.ToLower();
                    consultorRegistro.Apellidos = consultor.Apellidos.Trim();

                    context.SaveChanges();

                    return Ok(consultorRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            } else
            {
                return NotFound();
            }
        }

        // DELETE api/<ConsultorController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var consultor = context.Consultor.Find(id);
            if (consultor != null)
            {
                try
                {
                    context.Consultor.Remove(consultor);
                    context.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            } else
            {
                return NotFound();
            }
        }
    }
}
