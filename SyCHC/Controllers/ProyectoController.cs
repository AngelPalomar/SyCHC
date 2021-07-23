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
    public class ProyectoController : ControllerBase
    {
        private readonly AppDbContext context;
        public ProyectoController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<ProyectoController>
        [HttpGet]
        public IEnumerable<Proyecto> Get()
        {
            return context.Proyecto.ToList();
        }

        // GET api/<ProyectoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var proyecto = context.Proyecto.Find(id);
            if (proyecto != null)
            {
                return Ok(proyecto);
            }
            else
            {
                return NotFound("Proyecto no encontrado.");
            }
        }

        // GET api/<ProyectoController>/5
        [HttpGet("cliente/{idCliente}")]
        public ActionResult GetByIdCliente(Guid idCliente)
        {
            var proyecto = context.Proyecto.Where(p => p.IdCliente == idCliente);
            if (proyecto != null)
            {
                return Ok(proyecto);
            }
            else
            {
                return NotFound("Proyecto no encontrado.");
            }
        }

        // POST api/<ProyectoController>
        [HttpPost]
        public ActionResult Post([FromBody] Proyecto proyecto)
        {
            try
            {
                proyecto.UltimaModificacion = DateTime.Now;
                context.Proyecto.Add(proyecto);
                context.SaveChanges();

                return Ok(proyecto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        // PUT api/<ProyectoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Proyecto nuevoProyecto)
        {
            var proyectoRegistro = context.Proyecto.Find(id);
            if (proyectoRegistro != null)
            {
                try
                {
                    proyectoRegistro.Nombre = nuevoProyecto.Nombre;
                    proyectoRegistro.Descripcion = nuevoProyecto.Descripcion;
                    proyectoRegistro.IdCliente = nuevoProyecto.IdCliente;
                    proyectoRegistro.FechaInicio = nuevoProyecto.FechaInicio;
                    proyectoRegistro.FechaFinal = nuevoProyecto.FechaFinal;
                    proyectoRegistro.HorasEstimadas = nuevoProyecto.HorasEstimadas;
                    proyectoRegistro.HorasTotales = nuevoProyecto.HorasTotales;
                    proyectoRegistro.EtapaActual = nuevoProyecto.EtapaActual;
                    proyectoRegistro.EstadoActual = nuevoProyecto.EstadoActual;
                    proyectoRegistro.Moneda = nuevoProyecto.Moneda;
                    proyectoRegistro.PrecioEstimado = nuevoProyecto.PrecioEstimado;
                    proyectoRegistro.PrecioReal = nuevoProyecto.PrecioReal;
                    proyectoRegistro.CostoEstimado = nuevoProyecto.CostoEstimado;
                    proyectoRegistro.CostoReal = nuevoProyecto.CostoReal;
                    proyectoRegistro.ImagenURL = nuevoProyecto.ImagenURL;
                    proyectoRegistro.ModificadoPor = nuevoProyecto.ModificadoPor;
                    proyectoRegistro.UltimaModificacion = DateTime.Now;

                    context.SaveChanges();

                    return Ok(proyectoRegistro);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Proyecto no encontrado.");
            }
        }

        // DELETE api/<ProyectoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var proyecto = context.Proyecto.Find(id);
            if (proyecto != null)
            {
                try
                {
                    context.Proyecto.Remove(proyecto);
                    context.SaveChanges();

                    return Ok("Proyecto eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound("Proyecto no encontrado.");
            }
        }
    }
}
