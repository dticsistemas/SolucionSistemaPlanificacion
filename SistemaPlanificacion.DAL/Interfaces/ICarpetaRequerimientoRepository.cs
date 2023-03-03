using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.DAL.Interfaces
{
    public interface ICarpetaRequerimientoRepository : IGenericRepository<CarpetaRequerimiento>
    {
        Task<CarpetaRequerimiento> Registrar(CarpetaRequerimiento entidad);
        Task<List<DetalleCarpetum>> Reporte(DateTime FechaInicio, DateTime FechaFin);

    }
}
