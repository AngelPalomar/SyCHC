using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Consultores_Proyecto
    {
        public Guid IdConsultor { get; set; }
        public Guid IdProyecto { get; set; }
        public Guid IdPerfil { get; set; }
    }
}
