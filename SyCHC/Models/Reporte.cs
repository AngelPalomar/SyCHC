using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Reporte
    {
        public Guid IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string EstadoActual { get; set; }
        public string Moneda { get; set; }
        public Guid IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionFiscal { get; set; }
        public Guid IdConsultor { get; set; }
        public string NombreConsultor { get; set; }
        public DateTime FechaActividad { get; set; }
        public DateTime FechaActividadCorta { get; set; }
        public string DescripcionActividad { get; set; }
        public int HorasTrabajadas { get; set; }
        public bool HorasFacturables { get; set; }
        public Guid IdEtapa { get; set; }
        public string NombreEtapa { get; set; }
    }
}
