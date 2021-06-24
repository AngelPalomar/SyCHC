using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyCHC.Context;
using SyCHC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private readonly AppDbContext context;
        public ActividadController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
        }

        // GET: api/<ActividadController>
        [HttpGet]
        public IEnumerable<Actividad> Get()
        {
            return context.Actividad.ToList();
        }

        // GET api/<ActividadController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var actividad = context.Actividad.Find(id);
            if (actividad != null)
            {
                return Ok(actividad);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<ActividadController>
        [HttpPost]
        public ActionResult Post([FromBody] Actividad actividad)
        {
            //Guarda los datos de la actividad
            try
            {
                context.Actividad.Add(actividad);
                context.SaveChanges();

                return Ok(actividad);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ActividadController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Actividad nuevaActividad)
        {
            var actividadRegistro = context.Actividad.Find(id);
            if (actividadRegistro != null)
            {
                try
                {
                    actividadRegistro.IdProyecto = nuevaActividad.IdProyecto;
                    actividadRegistro.IdConsultor = nuevaActividad.IdConsultor;
                    actividadRegistro.Fecha = nuevaActividad.Fecha;
                    actividadRegistro.Descripcion = nuevaActividad.Descripcion;
                    actividadRegistro.Etapa = nuevaActividad.Etapa;
                    actividadRegistro.HorasTrabajadas = nuevaActividad.HorasTrabajadas;
                    actividadRegistro.ArchivoURL = nuevaActividad.ArchivoURL;
                    actividadRegistro.HorasFacturables = nuevaActividad.HorasFacturables;

                    context.SaveChanges();

                    return Ok(actividadRegistro);
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

        // DELETE api/<ActividadController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var actividad = context.Actividad.Find(id);
            if (actividad != null)
            {
                try
                {
                    context.Actividad.Remove(actividad);
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
