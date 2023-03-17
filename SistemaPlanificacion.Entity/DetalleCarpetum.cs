using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public partial class DetalleCarpetum
    {
        public int IdDetalle { get; set; }

        public int? IdCarpeta { get; set; }

        public string? Partida { get; set; }

        public int? Cantidad { get; set; }

        public string? Detalle { get; set; }

        public string? UnidadMedida { get; set; }

        public decimal? PrecioUnitario  { get; set; }

        public decimal? PrecioTotal { get; set; }

        public decimal? MontoPlanificado { get; set; }

        public decimal? MontoPresupuestado { get; set; }

        public decimal? MontoAdjudicado { get; set; }

        public virtual CarpetaRequerimiento? IdCarpetaNavigation { get; set; }

        public virtual DetalleCertificacionPoa? DetalleCertificacionPoa { get; set; }

    }

}
