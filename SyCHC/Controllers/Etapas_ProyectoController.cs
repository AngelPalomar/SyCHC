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
    public class Etapas_ProyectoController : ControllerBase
    {
        private readonly AppDbContext context;
        public Etapas_ProyectoController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<Etapas_ProyectoController>
        [HttpGet]
        public IEnumerable<Etapas_Proyecto> Get()
        {
            return context.Etapas_Proyecto.ToList();
        }

        // GET api/<Etapas_ProyectoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var etapaProyecto = context.Etapas_Proyecto.Find(id);
            if (etapaProyecto != null)
            {
                return Ok(etapaProyecto);
            }
            else
            {
                return NotFound("Etapa del proyecto no encontrada.");
            }
        }

        // GET api/<Perfiles_ConsultorController>lista-etapas/abc-defg
        [HttpGet("lista-etapas/{idProyecto}")]
        public ActionResult Get(Guid idProyecto)
        {
            var listaEtapas = context.Lista_Etapas_De_Proyecto.Where(lista => lista.IdProyecto == idProyecto);
            if (listaEtapas != null)
            {
                return Ok(listaEtapas);
            }
            else
            {
                return NotFound("Etapa del proyecto no encontrada.");
            }
        }

        // POST api/<Etapas_ProyectoController>
        [HttpPost]
        public ActionResult Post([FromBody] Etapas_Proyecto etapas_Proyecto)
        {
            //Búsqueda de etapa existente
            var existeEtapaEnProyecto = context.Etapas_Proyecto.FirstOrDefault
                (
                    ep => 
                    ep.IdEtapa == etapas_Proyecto.IdEtapa &&
                    ep.IdProyecto == etapas_Proyecto.IdProyecto
                );

            if (existeEtapaEnProyecto != null)
            {
                return BadRequest("Esta etapa ya pertenece a este proyecto.");
            }

            //Añade la etapa
            try
            {
                context.Etapas_Proyecto.Add(etapas_Proyecto);
                context.SaveChanges();

                return Ok(etapas_Proyecto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<Etapas_ProyectoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Etapas_ProyectoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var etapa_proyecto = context.Etapas_Proyecto.Find(id);
            if (etapa_proyecto != null)
            {
                try
                {
                    context.Etapas_Proyecto.Remove(etapa_proyecto);
                    context.SaveChanges();

                    return Ok("Etapa eliminada con éxito.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Etapa del proyecto no encontrada.");
            }
        }
    }
}
