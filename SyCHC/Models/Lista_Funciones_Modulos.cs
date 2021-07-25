using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Funciones_Modulos
    {
        public Guid Id { get; set; }
        public Guid IdModulo { get; set; }
        public string Accion { get; set; }
        public string NombreModulo { get; set; }
    }
}
