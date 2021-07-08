using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Usuarios_De_Cliente
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdUsuario { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreUsuario { get; set; }
    }
}
