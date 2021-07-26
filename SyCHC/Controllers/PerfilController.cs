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
    public class PerfilController : ControllerBase
    {
        private readonly AppDbContext context;
        public PerfilController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<PerfilController>
        [HttpGet]
        public IEnumerable<Perfil> Get([FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Perfiles"))
                return null;

            return context.Perfil.ToList();
        }

        // GET api/<PerfilController>/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Perfiles"))
                return null;

            var perfil = context.Perfil.FirstOrDefault(p => p.Id == id);
            if (perfil != null)
            {
                return Ok(perfil);
            } else
            {
                return NotFound();
            }
        }

        // POST api/<PerfilController>
        [HttpPost]
        public ActionResult Post([FromBody] Perfil perfil, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "crear", "Perfiles"))
                return BadRequest("No tiene permisos.");

            Perfil nuevoPerfil = new Perfil();
            try
            {
                nuevoPerfil.Nombre = perfil.Nombre;
                nuevoPerfil.Nivel = perfil.Nivel;
                nuevoPerfil.Costo = Convert.ToDecimal(perfil.Costo);

                context.Perfil.Add(nuevoPerfil);
                context.SaveChanges();

                return Ok(perfil);
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<PerfilController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Perfil perfil, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "modificar", "Perfiles"))
                return BadRequest("No tiene permisos");

            var perfilRegistro = context.Perfil.Find(id);

            if (perfilRegistro != null)
            {
                try
                {
                    perfilRegistro.Nombre = perfil.Nombre;
                    perfilRegistro.Nivel = perfil.Nivel;
                    perfilRegistro.Costo = Convert.ToDecimal(perfil.Costo);

                    context.SaveChanges();

                    return Ok(perfilRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Perfil no encontrado.");
            }
        }

        // DELETE api/<PerfilController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "eliminar", "Perfiles"))
                return BadRequest("No tiene permisos");

            var perfil = context.Perfil.Find(id);

            if (perfil != null)
            {
                try
                {
                    context.Perfil.Remove(perfil);
                    context.SaveChanges();

                    return Ok("Perfil eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            } 
            else
            {
                return NotFound("Perfil no encontrado.");
            }
        }
    }
}
