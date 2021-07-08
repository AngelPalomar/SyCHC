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
        public IEnumerable<Funcion> Get()
        {
            return context.Funcion.ToList();
        }

        // GET api/<FuncionController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
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
        public ActionResult Post([FromBody] Funcion funcion)
        {
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
        public ActionResult Put(Guid id, [FromBody] Funcion nuevaFuncion)
        {
            var funcionRegistro = context.Funcion.Find(id);
            if (funcionRegistro != null)
            {
                try
                {
                    funcionRegistro.Nombre = nuevaFuncion.Nombre;
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
        public ActionResult Delete(Guid id)
        {
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
