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
            var usuario = context.Usuario.Find(consultor.Id);
            if (usuario != null)
            {
                try
                {
                    //Asigno tipo usuario
                    usuario.AsignadoTipoUsuario = true;

                    //Guardo el consultor
                    context.Consultor.Add(consultor);
                    context.SaveChanges();

                    return Ok(consultor);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Usuario consultor no encontrado.");
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
                    consultorRegistro.Nombres = consultor.Nombres.Trim();
                    consultorRegistro.Apellidos = consultor.Apellidos.Trim();

                    context.SaveChanges();

                    return Ok("Consultor modificado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            } else
            {
                return NotFound("Consultor no encontrado.");
            }
        }

        // DELETE api/<ConsultorController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var consultor = context.Consultor.Find(id);
            var usuario = context.Usuario.Find(id);

            if (consultor != null && usuario != null)
            {
                try
                {
                    //Elimino consultor
                    context.Consultor.Remove(consultor);

                    //Des-asigno tipo usuario
                    usuario.AsignadoTipoUsuario = false;

                    context.SaveChanges();

                    return Ok("Consultor eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            } else
            {
                return NotFound("Consultor no encontrado.");
            }
        }
    }
}
