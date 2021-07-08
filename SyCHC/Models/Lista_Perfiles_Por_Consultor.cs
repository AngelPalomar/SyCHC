using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Perfiles_Por_Consultor
    {
        public int Id { get; set; }
        public Guid IdPerfil { get; set; }
        public Guid IdConsultor { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public int AniosExperiencia { get; set; }
        public decimal Costo { get; set; }
    }
}
