using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string NombreUsuario { get; set; }
        public bool Estado { get; set; }
        public int TipoUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public bool AsignadoTipoUsuario { get; set; }
    }
}
