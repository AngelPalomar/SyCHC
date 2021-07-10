using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionFiscal { get; set; }
        public string ImagenURL { get; set; }
    }
}
