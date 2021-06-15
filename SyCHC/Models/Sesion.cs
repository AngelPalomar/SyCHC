using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Sesion
    {
        public Guid IdUsuario { get; set; }
        public string DireccionIP { get; set; }
        public DateTime Fecha { get; set; }
        public Guid Key { get; set; }
    }
}
