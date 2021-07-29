using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Actividades_Consultor_Proyecto
    {
        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public Guid IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public Guid IdConsultor { get; set; }
        public string NombreConsultor { get; set; }
    }
}
