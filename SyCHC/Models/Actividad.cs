using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Actividad
    {
        public Guid Id { get; set; }
        public Guid IdProyecto { get; set; }
        public Guid IdConsultor { get; set; }
        public DateTime? Fecha { get; set; }
        public string Descripcion { get; set; }
        public Guid Etapa { get; set; }
        public int HorasTrabajadas { get; set; }
        public string ArchivoURL { get; set; }
        public bool HorasFacturables { get; set; }
    }
}
