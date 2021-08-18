using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Acceso
    {
        [Key]
        public long Id { get; set; }
        public string IdTipoUsuario { get; set; }
        public Guid IdFuncion { get; set; }
        public bool Estado { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? UltimaModificacion { get; set; }
    }
}
