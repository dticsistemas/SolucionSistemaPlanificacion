using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class UnidadResponsable
    {
        
        public int IdUnidad { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }

        public bool? EsActivo { get; set; }

        public DateTime? FechaRegistro { get; set; }

    }
}
