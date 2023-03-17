using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleCertificacionPoa
    {
        public int? IdCertificacion { get; set; }

        public int IdDetalle { get; set; }

        public decimal? MontoPresupuesto { get; set; }

        public virtual CertificacionPoa? IdCertificacionNavigation { get; set; }

        public virtual DetalleCarpetum IdDetalleNavigation { get; set; } = null!;
    }
}
