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
        public int Id { get; set; }
        public Guid IdUsuarios { get; set; }
        public Guid IdCliente { get; set; }
    }
}
