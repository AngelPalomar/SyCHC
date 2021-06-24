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
    public class SesionController : ControllerBase
    {
        private readonly AppDbContext context;
        public SesionController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<SesionController>
        [HttpGet]
        public IEnumerable<Sesion> Get()
        {
            return context.Sesion.ToList();
        }

        // GET api/<SesionController>/5
        [HttpGet("{idUsuario}")]
        public ActionResult Get(Guid idUsuario)
        {
            var sesion = context.Sesion.Where(s => s.IdUsuario == idUsuario);
            if (sesion.ToList().Count > 0)
            {
                return Ok(sesion);
            }
            else
            {
                return NotFound("No hay sesiones existentes.");
            }
        }

        // POST api/<SesionController>
        [HttpPost]
        public ActionResult Post([FromBody] Sesion sesion)
        {
            try
            {
                context.Sesion.Add(sesion);
                context.SaveChanges();

                return Ok(sesion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SesionController>/5
        //[HttpPut("{id}")]
        //public void Put(Guid id, [FromBody] string value)
        //{
        //}

        // DELETE api/<SesionController>/5
        [HttpDelete("{clave}")]
        public ActionResult Delete(Guid clave)
        {
            var sesion = context.Sesion.Find(clave);
            if (sesion != null)
            {
                try
                {
                    context.Sesion.Remove(sesion);
                    context.SaveChanges();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
