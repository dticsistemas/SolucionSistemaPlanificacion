using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public class DetalleCertificacionPoa
    {
        public int? IdCertificacion { get; set; }

        public int IdDetalle { get; set; }

        public decimal? MontoPresupuesto { get; set; }

        public virtual CertificacionPoa? IdCertificacionNavigation { get; set; }

        public virtual DetalleCarpetum IdDetalleNavigation { get; set; } = null!;
    }
}
