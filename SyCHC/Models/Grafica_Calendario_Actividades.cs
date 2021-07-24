using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Grafica_Calendario_Actividades
    {
        public Guid IdProyecto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaActividadCorta { get; set; }
    }
}
