using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Perfil
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public decimal Costo { get; set; }
    }
}
