using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    [Keyless]
    public class Lista_Consultores_De_Proyecto
    {
        public int Id { get; set; }
        public Guid IdProyecto { get; set; }
        public string NombresConsultor { get; set; }
        public string ApellidosConsultor { get; set; }
        public string NombrePerfil { get; set; }
    }
}
