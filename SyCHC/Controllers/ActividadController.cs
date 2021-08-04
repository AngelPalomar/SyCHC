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
        public IEnumerable<Actividad> Get([FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Actividades"))
                return null;

            return context.Actividad.ToList();
        }

        // GET api/<ActividadController>/5
        [HttpGet("{id}")]
        public ActionResult GetById(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Actividades"))
                return BadRequest("No tiene permisos");

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

        // GET api/<ActividadController>/lista-actividades/5
        [HttpGet("lista-actividades/{idProyecto}")]
        public ActionResult GetActividadesDeProyecto(Guid idProyecto, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "ver", "Actividades"))
                return BadRequest("No tiene permisos");

            var actividad = context
                .Lista_Actividades_Consultor_Proyecto
                .Where(ac => ac.IdProyecto == idProyecto);
            if (actividad != null)
            {
                return Ok(actividad.OrderByDescending(ac => ac.Fecha));
            }
            else
            {
                return NotFound("No hay actividades.");
            }
        }

        // POST api/<ActividadController>
        [HttpPost]
        public ActionResult Post([FromBody] Actividad actividad, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "crear", "Actividades"))
                return BadRequest("No tiene permisos");

            //Guarda los datos de la actividad
            try
            {
                //Compara las fechas para ver si la inicio es mayor a la final
                if (actividad.FechaInicio > actividad.FechaFinal)
                {
                    return BadRequest("La fecha final debe ser mayor a la fecha inicial.");
                }

                //Resta las fechas para ver si es menor a una semana
                if (actividad.FechaInicio.AddDays(7) < actividad.FechaFinal)
                {
                    return BadRequest("El periodo entre fecha de inicio y final debe ser igual o menor a 7 días.");
                }

                actividad.Fecha = DateTime.Now;
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
        public ActionResult Put(Guid id, [FromBody] Actividad nuevaActividad, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "modificar", "Actividades"))
                return BadRequest("No tiene permisos");

            var actividadRegistro = context.Actividad.Find(id);
            if (actividadRegistro != null)
            {
                try
                {
                    //Compara las fechas para ver si la inicio es mayor a la final
                    if (nuevaActividad.FechaInicio > nuevaActividad.FechaFinal)
                    {
                        return BadRequest("La fecha final debe ser mayor a la fecha inicial.");
                    }

                    //Resta las fechas para ver si es menor a una semana
                    if (nuevaActividad.FechaInicio.AddDays(7) < nuevaActividad.FechaFinal)
                    {
                        return BadRequest("El periodo entre fecha de inicio y final debe ser igual o menor a 7 días.");
                    }

                    actividadRegistro.IdProyecto = nuevaActividad.IdProyecto;
                    actividadRegistro.IdConsultor = nuevaActividad.IdConsultor;
                    actividadRegistro.Fecha = nuevaActividad.Fecha;
                    actividadRegistro.FechaInicio = nuevaActividad.FechaInicio;
                    actividadRegistro.FechaFinal = nuevaActividad.FechaFinal;
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
        public ActionResult Delete(Guid id, [FromHeader] Guid session_id)
        {
            //Verifica accesos
            AccesoController ac = new AccesoController(context);
            if (!ac.TieneAcceso(session_id, "eliminar", "Actividades"))
                return BadRequest("No tiene permisos");

            var actividad = context.Actividad.Find(id);
            if (actividad != null)
            {
                try
                {
                    context.Actividad.Remove(actividad);
                    context.SaveChanges();

                    return Ok("Actividad eliminada correctamente.");
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
