using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Info_Sesion
    {
        public Guid Clave { get; set; }
        public Guid IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreUsuario { get; set; }
        public string TipoUsuario { get; set; }
    }
}
