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
    public class FuncionController : ControllerBase
    {
        private readonly AppDbContext context;
        public FuncionController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<FuncionController>
        [HttpGet]
        public IEnumerable<Lista_Funciones_Modulos> Get([FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Funciones"))
                return null;

            return context.Lista_Funciones_Modulos.ToList();
        }

        // GET api/<FuncionController>/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Funciones"))
                return BadRequest("No tiene permisos");

            var funcion = context.Funcion.Find(id);
            if (funcion != null)
            {
                return Ok(funcion);
            }
            else
            {
                return NotFound("Función no encontrada.");
            }
        }

        // POST api/<FuncionController>
        [HttpPost]
        public ActionResult Post([FromBody] Funcion funcion, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "crear", "Funciones"))
                return BadRequest("No tiene permisos");

            //Verifica que no se duplique
            var existeFuncion = context
                .Funcion
                .FirstOrDefault(
                    f =>
                    f.Accion == funcion.Accion &&
                    f.IdModulo == funcion.IdModulo
                );

            if (existeFuncion != null)
                return BadRequest("Este modulo ya está asignado a esta función.");

            try
            {
                funcion.UltimaModificacion = DateTime.Now;
                context.Funcion.Add(funcion);
                context.SaveChanges();

                return Ok(funcion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<FuncionController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Funcion nuevaFuncion, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "modificar", "Funciones"))
                return BadRequest("No tiene permisos");

            //Verifica que no se duplique
            var existeFuncion = context
                .Funcion
                .FirstOrDefault(
                    f =>
                    f.Accion == nuevaFuncion.Accion &&
                    f.IdModulo == nuevaFuncion.IdModulo
                );

            if (existeFuncion != null)
                return BadRequest("Este modulo ya está asignado a esta función.");

            var funcionRegistro = context.Funcion.Find(id);
            if (funcionRegistro != null)
            {
                try
                {
                    funcionRegistro.Accion = nuevaFuncion.Accion;
                    funcionRegistro.IdModulo = nuevaFuncion.IdModulo;
                    funcionRegistro.ModificadoPor = nuevaFuncion.ModificadoPor;
                    funcionRegistro.UltimaModificacion = DateTime.Now;

                    context.SaveChanges();

                    return Ok(funcionRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Función no encontrada.");
            }
        }

        // DELETE api/<FuncionController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "eliminar", "Funciones"))
                return BadRequest("No tiene permisos");

            var funcion = context.Funcion.Find(id);
            if (funcion != null)
            {
                try
                {
                    context.Funcion.Remove(funcion);
                    context.SaveChanges();

                    return Ok("Función eliminada correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Función no encontrada.");
            }
        }
    }
}
