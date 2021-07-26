using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        private readonly AppDbContext context;
        public ModuloController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ModuloController>
        [HttpGet]
        public IEnumerable<Modulo> Get([FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Modulos"))
                return null;

            return context.Modulo.ToList();
        }

        // GET api/<ModuloController>/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Modulos"))
                return BadRequest("No tiene permisos");

            var modulo = context.Modulo.Find(id);
            if (modulo != null)
            {
                return Ok(modulo);
            }
            else
            {
                return NotFound("Módulo no encontrado");
            }
        }

        // POST api/<ModuloController>
        [HttpPost]
        public ActionResult Post([FromBody] Modulo modulo, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "crear", "Modulos"))
                return BadRequest("No tiene permisos");

            try
            {
                modulo.Nombre = modulo.Nombre.Trim();
                modulo.UltimaModificacion = DateTime.Now;
                context.Modulo.Add(modulo);
                context.SaveChanges();

                return Ok(modulo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ModuloController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Modulo nuevoModulo, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "modificar", "Modulos"))
                return BadRequest("No tiene permisos");

            var moduloRegistro = context.Modulo.Find(id);
            if (moduloRegistro != null)
            {
                try
                {
                    moduloRegistro.Nombre = nuevoModulo.Nombre.Trim();
                    moduloRegistro.ModificadoPor = nuevoModulo.ModificadoPor;
                    moduloRegistro.UltimaModificacion = DateTime.Now;

                    context.SaveChanges();

                    return Ok(moduloRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Módulo no encontrado.");
            }
        }

        // DELETE api/<ModuloController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "eliminar", "Modulos"))
                return BadRequest("No tiene permisos");

            var moduloRegistro = context.Modulo.Find(id);
            if (moduloRegistro != null)
            {
                try
                {
                    context.Modulo.Remove(moduloRegistro);
                    context.SaveChanges();

                    return Ok("Modulo eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Módulo no encontrado.");
            }
        }
    }
}
