using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Accesos_Modulo_Tipo_Usuario
    {
        public string TipoUsuario { get; set; }
        public string Accion { get; set; }
        public string NombreModulo { get; set; }
        public bool Estado { get; set; }
    }
}
