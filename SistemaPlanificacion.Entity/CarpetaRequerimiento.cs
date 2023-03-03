﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.Entity
{
    public partial class CarpetaRequerimiento
    {
        public int IdCarpeta { get; set; }

        public string? NumeroCarpeta { get; set; }

        public int? IdRegional { get; set; }

        public string? CiteUnidadPlanificacion { get; set; }

        public int? TipoSolicitante { get; set; }

        public int? UnidadSolicitante { get; set; }

        public string? CertificadoPoa { get; set; }

        public int? UnidadResponsable { get; set; }

        public int? CodOperacion { get; set; }

        public string? Operacion { get; set; }

        public int? IdActividad { get; set; }

        public decimal? MontoTotal { get; set; }

        public decimal? MontoTotalPlanificacion { get; set; }

        public string? Tipo { get; set; }

        public string? Estado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<DetalleCarpetum> DetalleCarpeta { get; } = new List<DetalleCarpetum>();
    }
}