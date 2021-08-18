using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyCHC.Models
{
    public class Etapas_Proyecto
    {
        [Key]
        public long Id { get; set; }
        public Guid IdEtapa { get; set; }
        public Guid IdProyecto { get; set; }
    }
}
