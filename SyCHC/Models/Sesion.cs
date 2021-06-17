using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Sesion
    {
        [Key]
        public Guid Clave { get; set; }
        public Guid IdUsuario { get; set; }
        public string DireccionIP { get; set; }
        public DateTime Fecha { get; set; }
    }
}
