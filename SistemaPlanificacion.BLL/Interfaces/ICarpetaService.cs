using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface ICarpetaService
    {
        Task<List<PartidaPresupuestaria>> ObtenerPartidasPresupuestarias(string busqueda);
        Task<CarpetaRequerimiento> Registrar(CarpetaRequerimiento entidad);
        Task<List<CarpetaRequerimiento>> Historial(string numeroCarpeta, string fechaInicio, string fechaFin);
        Task<CarpetaRequerimiento> Detalle(string numeroCarpeta);
        Task<List<DetalleCarpetum>> Reporte(string fechaInicio, string fechaFin);
        Task<List<CarpetaRequerimiento>> Lista();
    }
}
