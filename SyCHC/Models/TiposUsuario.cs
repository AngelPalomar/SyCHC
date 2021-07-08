using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class TiposUsuario
    {
        [Key]
        public string Tipo { get; set; }
    }
}
