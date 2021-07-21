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
        public ActionResult GetReporteProyecto(Guid? idProyecto, Guid? idEtapa, Guid? idConsultor, DateTime? fecha1, DateTime? fecha2)
        {

            //Validacion de id proyecto
            if (!idProyecto.HasValue)
                return BadRequest("Seleccione un proyecto.");

            //Busca el proyecto
            var proyecto = context.Reporte.Where(rp => rp.IdProyecto == idProyecto);

            if (proyecto == null)
                return NotFound("Proyecto no encontrado");

            //Verifica los valores que se dieron, irá haciendo la consulta comforme a los filtros dados
            if (idEtapa.HasValue)
            {
                proyecto = proyecto.Where(rp => rp.IdEtapa == idEtapa);
            }

            if (idConsultor.HasValue)
            {
                proyecto = proyecto.Where(rp => rp.IdConsultor == idConsultor);
            }

            if (fecha1.HasValue || fecha2.HasValue)
            {
                if (fecha1.HasValue && fecha2.HasValue)
                {
                    proyecto = proyecto.Where(rp => rp.FechaActividad >= fecha1 && rp.FechaActividad <= fecha2.Value.AddDays(1));
                }
                else
                {
                    return BadRequest("Las dos fechas son obligatorias, si desea actividades de un solo día, ingrese la misma fecha en los dos campos.");
                }
            }

            //Retorna el proyecto
            return Ok(proyecto);

        }

        //[HttpGet("proyecto/actividades-por-dia")]
        //public ActionResult GetDesgloseActividadesPorDia(Guid? idProyecto, Guid? idConsultor, DateTime? fecha1, DateTime? fecha2)
        //{
        //    if (!idProyecto.HasValue)
        //        return BadRequest();

        //    //Busca por proyecto
        //    var desglose = context.Desglose_Reporte_Actividades_Por_Dia.Where
        //        (
        //            draph =>
        //                draph.IdProyecto == idProyecto 
        //        );

        //    //Aplica los filtros
        //    if (idConsultor.HasValue)
        //    {
        //        desglose = desglose = context.Desglose_Reporte_Actividades_Por_Dia.Where
        //        (
        //            draph =>
        //                draph.IdConsultor == idConsultor
        //        );
        //    }

        //    if (fecha1.HasValue || fecha2.HasValue)
        //    {
        //        if (fecha1.HasValue && fecha2.HasValue)
        //        {
        //            desglose = desglose.Where(draph => draph.FechaActividadCorta >= fecha1 && draph.FechaActividadCorta <= fecha2.Value.AddDays(1));
        //        }
        //        else
        //        {
        //            return BadRequest("Las dos fechas son obligatorias, si desea actividades de un solo día, ingrese la misma fecha en los dos campos.");
        //        }
        //    }

        //    return Ok(desglose);
        //}
    }
}
