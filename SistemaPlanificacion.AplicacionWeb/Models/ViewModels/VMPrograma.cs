﻿using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Models.ViewModels
{
    public class VMPrograma
    {
        public int IdPrograma { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? EsActivo { get; set; }
    }
}
