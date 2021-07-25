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
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext context;
        public AccesoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<AccesoController>
        [HttpGet]
        public IEnumerable<Acceso> Get()
        {
            return context.Acceso.ToList();
        }

        // GET api/<AccesoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var acceso = context.Acceso.Find(id);
            if (acceso != null)
            {
                return Ok(acceso);
            }
            else
            {
                return NotFound("Acceso no encontrado.");
            }
        }

        // POST api/<AccesoController>
        [HttpPost]
        public ActionResult Post([FromBody] Acceso acceso)
        {
            //Valida si ya existe un acceso igual
            var existeAcceso = context.Acceso.FirstOrDefault
                (
                    acc => 
                    acc.IdTipoUsuario == acceso.IdTipoUsuario &&
                    acc.IdFuncion == acceso.IdFuncion
                );

            if (existeAcceso != null)
                return BadRequest($"Este acceso ya está asignado al tipo de usuario {acceso.IdTipoUsuario}");

            //Intenta guardar el nuevo acceso
            try
            {
                acceso.UltimaModificacion = DateTime.Now;
                context.Acceso.Add(acceso);
                context.SaveChanges();

                return Ok(acceso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AccesoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Acceso nuevoAcceso)
        {
            //Valida si ya existe un acceso igual
            var existeAcceso = context.Acceso.FirstOrDefault
                (
                    acc =>
                    acc.IdTipoUsuario == nuevoAcceso.IdTipoUsuario &&
                    acc.IdFuncion == nuevoAcceso.IdFuncion
                );

            if (existeAcceso != null)
                return BadRequest($"Este acceso ya está asignado al tipo de usuario {nuevoAcceso.IdTipoUsuario}");

            var accesoRegistro = context.Acceso.Find(id);
            if (accesoRegistro != null)
            {
                try
                {
                    accesoRegistro.IdTipoUsuario = nuevoAcceso.IdTipoUsuario;
                    accesoRegistro.IdFuncion = nuevoAcceso.IdFuncion;
                    accesoRegistro.Estado = nuevoAcceso.Estado;
                    accesoRegistro.ModificadoPor = nuevoAcceso.ModificadoPor;
                    accesoRegistro.UltimaModificacion = DateTime.Now;

                    context.SaveChanges();

                    return Ok(accesoRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Acceso no encontrado.");
            }
        }

        // DELETE api/<AccesoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var accesoRegistro = context.Acceso.Find(id);
            if (accesoRegistro != null)
            {
                try
                {
                    context.Acceso.Remove(accesoRegistro);
                    context.SaveChanges();

                    return Ok("Acceso eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Acceso no encontrado.");
            }
        }
    }
}
