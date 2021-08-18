using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Clientes_Usuario
    {
        [Key]
        public long Id { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdCliente { get; set; }
    }
}
