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
    public class Consultores_ProyectoController : ControllerBase
    {
        private readonly AppDbContext context;
        public Consultores_ProyectoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<Consultores_ProyectoController>
        [HttpGet]
        public IEnumerable<Consultores_Proyecto> Get()
        {
            return context.Consultores_Proyecto.ToList();
        }

        // GET api/<Consultores_ProyectoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var consultoresProyecto = context.Consultores_Proyecto.Find(id);
            if (consultoresProyecto != null)
            {
                return Ok(consultoresProyecto);
            }
            else
            {
                return NotFound("No hay proyectos.");
            }
        }

        // GET api/<Consultores_ProyectoController>/lista-proyectos/5
        [HttpGet("lista-proyectos/{idConsultor}")]
        public ActionResult GetProyectosPorConsultor(Guid idConsultor)
        {
            var consultoresProyecto = context
                .Lista_Proyectos_Cliente_Consultor
                .Where(cp => cp.IdConsultor == idConsultor);

            if (consultoresProyecto.Count() > 0)
            {
                return Ok(consultoresProyecto);
            }
            else
            {
                return NotFound("No hay proyectos.");
            }
        }

        // GET api/<Consultores_ProyectoController>/lista-proyectos/5
        [HttpGet("lista-consultores/{idProyecto}")]
        public ActionResult GetConsultoresPorProyecto(Guid idProyecto)
        {
            var consultoresProyecto = context
                .Lista_Consultores_De_Proyecto
                .Where(cp => cp.IdProyecto == idProyecto);

            if (consultoresProyecto.Count() > 0)
            {
                return Ok(consultoresProyecto);
            }
            else
            {
                return NotFound("No hay proyectos.");
            }
        }

        // POST api/<Consultores_ProyectoController>
        [HttpPost]
        public ActionResult Post([FromBody] Consultores_Proyecto consultores_Proyecto)
        {
            //Validación si el consultor ya existe en el proyecto
            var existeConsultorProyecto = context
                .Consultores_Proyecto
                .FirstOrDefault(cc => 
                    cc.IdConsultor == consultores_Proyecto.IdConsultor && 
                    cc.IdProyecto == consultores_Proyecto.IdProyecto
                );

            if (existeConsultorProyecto != null)
            {
                return BadRequest("Este consultor ya existe en este proyecto.");
            }

            try
            {
                context.Add(consultores_Proyecto);
                context.SaveChanges();

                return Ok(consultores_Proyecto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<Consultores_ProyectoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Consultores_Proyecto nuevoConsultores_Proyecto)
        {
            var consultoresProyectoRegistro = context.Consultores_Proyecto.Find(id);
            if (consultoresProyectoRegistro != null)
            {
                try
                {
                    consultoresProyectoRegistro.IdConsultor = nuevoConsultores_Proyecto.IdConsultor;
                    consultoresProyectoRegistro.IdProyecto = nuevoConsultores_Proyecto.IdProyecto;
                    consultoresProyectoRegistro.IdPerfil = nuevoConsultores_Proyecto.IdPerfil;

                    context.SaveChanges();

                    return Ok(consultoresProyectoRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("No existe este consultor con este proyecto.");
            }
        }

        // DELETE api/<Consultores_ProyectoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var consultorProyecto = context.Consultores_Proyecto.Find(id);
            if (consultorProyecto != null)
            {
                try
                {
                    context.Consultores_Proyecto.Remove(consultorProyecto);
                    context.SaveChanges();

                    return Ok("Consultor eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return NotFound("No existe este consultor con este proyecto.");
            }
        }
    }
}
