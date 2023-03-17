using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class CertificacionPoa
    {
        public int IdCertificacionPoa { get; set; }

        public int? IdCarpeta { get; set; }

        public string? Codigo { get; set; }

        public decimal? TotalCertificado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string? EstadoCertificacion { get; set; }

        public int? IdUsuario { get; set; }

        public virtual ICollection<DetalleCertificacionPoa> DetalleCertificacionPoas { get; set; } = new List<DetalleCertificacionPoa>();

        //public virtual CarpetaRequerimiento IdCertificacionPoaNavigation { get; set; } = null!;


    }
}
