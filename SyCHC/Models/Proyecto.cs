using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Proyecto
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int HorasEstimadas { get; set; }
        public int HorasTotales { get; set; }
        public Guid? EtapaActual { get; set; }
        public string EstadoActual { get; set; }
        public string Moneda { get; set; }
        public decimal PrecioEstimado { get; set; }
        public decimal PrecioReal { get; set; }
        public decimal CostoEstimado { get; set; }
        public decimal CostoReal { get; set; }
        public string ImagenURL { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? UltimaModificacion { get; set; }
    }
}
