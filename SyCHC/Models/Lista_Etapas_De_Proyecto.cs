using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Etapas_De_Proyecto
    {
        public int Id { get; set; }
        public Guid IdProyecto { get; set; }
        public string Nombre { get; set; }
    }
}
