using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Proyectos_Cliente_Consultor
    {
        public Guid IdProyecto { get; set; }
        public Guid IdConsultor { get; set; }
        public Guid IdCliente { get; set; }
        public string NombreProyecto { get; set; }
        public string EstadoActual { get; set; }
        public string NombreCliente{ get; set; }
    }
}
