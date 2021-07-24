using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Grafica_Cantidad_Usuarios
    {
        public int Cantidad { get; set; }
        public string TipoUsuario { get; set; }
    }
}
