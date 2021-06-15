using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Perfiles_Consultor
    {
        public Guid IdConsultor { get; set; }
        public Guid IdPerfil { get; set; }
        public int AniosExperiencia { get; set; }
    }
}
