using Microsoft.AspNetCore.Mvc;
using SyCHC.Models;
using SyCHC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly AppDbContext context;
        public ReporteController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("proyecto")]
        public ActionResult GetReporteProyecto(Guid? idCliente, Guid? idProyecto, Guid? idEtapa, Guid? idConsultor, DateTime? fecha1, DateTime? fecha2)
        {

            //Validacion de id proyecto y cliente
            if (!idCliente.HasValue)
                return BadRequest("Seleccione un cliente.");

            if (!idProyecto.HasValue)
                return BadRequest("Seleccione un proyecto.");

            //Busca el proyecto
            var reporte = context.Reporte.Where(rp => rp.IdProyecto == idProyecto);

            if (reporte == null)
                return NotFound("Proyecto no encontrado");

            //Verifica los valores que se dieron, irá haciendo la consulta comforme a los filtros dados
            if (idEtapa.HasValue)
            {
                reporte = reporte.Where(rp => rp.IdEtapa == idEtapa);
            }

            if (idConsultor.HasValue)
            {
                reporte = reporte.Where(rp => rp.IdConsultor == idConsultor);
            }

            if (fecha1.HasValue || fecha2.HasValue)
            {
                if (fecha1.HasValue && fecha2.HasValue)
                {
                    reporte = reporte.Where(rp => rp.FechaActividad >= fecha1 && rp.FechaActividad <= fecha2.Value.AddDays(1));
                }
                else
                {
                    return BadRequest("Las dos fechas son obligatorias, si desea actividades de un solo día, ingrese la misma fecha en los dos campos.");
                }
            }

            //Retorna el proyecto
            return Ok(reporte);

        }

        [HttpGet("consultor")]
        public ActionResult GetReporteConsultor(Guid? idConsultor, Guid? idCliente, Guid? idProyecto, DateTime? fecha1, DateTime? fecha2)
        {
            //Validacion de id consultor
            if (!idConsultor.HasValue)
                return BadRequest("Seleccione un consultor.");

            //Busca el consultor
            var reporte = context.Reporte.Where(rp => rp.IdConsultor == idConsultor);

            if (reporte == null)
                return NotFound("Consultor no encontrado");

            //Valida y aplica los filtros
            if (idCliente.HasValue)
                reporte = reporte.Where(rp => rp.IdCliente == idCliente);

            if (idProyecto.HasValue)
                reporte = reporte.Where(rp => rp.IdProyecto == idProyecto);

            if (fecha1.HasValue || fecha2.HasValue)
            {
                if (fecha1.HasValue && fecha2.HasValue)
                    reporte = reporte.Where(rp => rp.FechaActividad >= fecha1 && rp.FechaActividad <= fecha2.Value.AddDays(1));
                else
                    return BadRequest("Las dos fechas son obligatorias, si desea actividades de un solo día, ingrese la misma fecha en los dos campos.");
            }

            return Ok(reporte);
        }

        [HttpGet("cliente")]
        public ActionResult GetReporteCliente(Guid? idCliente, Guid? idProyecto, Guid? idConsultor, DateTime? fecha1, DateTime? fecha2)
        {
            //Validacion de id proyecto
            if (!idCliente.HasValue)
                return BadRequest("Seleccione un cliente.");

            //Busca el consultor
            var reporte = context.Reporte.Where(rp => rp.IdCliente == idCliente);

            if (reporte == null)
                return NotFound("Cliente no encontrado");

            //Aplica los filtros

            if (idProyecto.HasValue)
                reporte = reporte.Where(rp => rp.IdProyecto == idProyecto);

            if (idConsultor.HasValue)
                reporte = reporte.Where(rp => rp.IdConsultor == idConsultor);

            if (fecha1.HasValue || fecha2.HasValue)
            {
                if (fecha1.HasValue && fecha2.HasValue)
                    reporte = reporte.Where(rp => rp.FechaActividad >= fecha1 && rp.FechaActividad <= fecha2.Value.AddDays(1));
                else
                    return BadRequest("Las dos fechas son obligatorias, si desea actividades de un solo día, ingrese la misma fecha en los dos campos.");
            }

            return Ok(reporte);
        }

        //GET GRAFICA
        [HttpGet("grafica-semana")]
        public IEnumerable<Grafica_Cantidad_Actividades_Semana> GetGraficaCantidadProyectos()
        {
            return context.Grafica_Cantidad_Actividades_Semana.ToList();
        }
    }
}
