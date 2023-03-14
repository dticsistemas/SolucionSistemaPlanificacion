using Microsoft.EntityFrameworkCore;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class CarpetaService : ICarpetaService
    {
        private IGenericRepository<PartidaPresupuestaria> _repositorioPartidaPresupuestaria;
        private readonly ICarpetaRequerimientoRepository _repositorioCarpeta;

        public CarpetaService(IGenericRepository<PartidaPresupuestaria> repositorioPartida, ICarpetaRequerimientoRepository
            repositorioCarpeta)
        {
            _repositorioPartidaPresupuestaria = repositorioPartida;
            _repositorioCarpeta = repositorioCarpeta;
        }
        public async Task<List<PartidaPresupuestaria>> ObtenerPartidasPresupuestarias(string busqueda)
        {
            //IQueryable<PartidaPresupuestaria> query = await _repositorioPartidaPresupuestaria.Consultar(
            //p=>p.EsActivo == true //&&
            //p.Stock>0 && 
            // string.Concat(p.Codigo,p.Nombre,p.DetallePlanificacions).Contains(busqueda)
            //);
            //return query.ToList();
            IQueryable<PartidaPresupuestaria> query = await _repositorioPartidaPresupuestaria.Consultar(
                p => p.EsActivo == true &&
                string.Concat(p.Codigo, p.Nombre).Contains(busqueda)
                );
            return query.ToList();
        }
        public async Task<CarpetaRequerimiento> Registrar(CarpetaRequerimiento entidad)
        {
            try
            {
                return await _repositorioCarpeta.Registrar(entidad);
            }
            catch
            {
                throw;
            }

        }
        public async Task<List<CarpetaRequerimiento>> Historial(string numeroCarpeta, string fechaInicio, string fechaFin)
        {
            IQueryable<CarpetaRequerimiento> query = await _repositorioCarpeta.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            if (fechaInicio != "" && fechaFin != "")
            {
                DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_fin.Date
                )
                    // .Include(tdv => tdv.IdTipoDocuemnt)                    
                     .Include(dv => dv.DetalleCarpeta)
                    .ToList();
            }
            else
            {
                return query.Where(v => v.NumeroCarpeta == numeroCarpeta
                )
                    //.Include(tdv => tdv.IdTipoDocument)                    
                    .Include(dv => dv.DetalleCarpeta)
                    .ToList();
            }
        }
        public async Task<CarpetaRequerimiento> Detalle(string numeroCarpeta)
        {
            IQueryable<CarpetaRequerimiento> query = await _repositorioCarpeta.Consultar(v => v.NumeroCarpeta == numeroCarpeta);
            return query
                .Include(dv => dv.DetalleCarpeta)
                .First();
        }
        public async Task<List<DetalleCarpetum>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fech_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/YY", new CultureInfo("es-PE"));
            DateTime fech_fin = DateTime.ParseExact(fechaFin, "dd/MM/YY", new CultureInfo("es-PE"));

            List<DetalleCarpetum> lista = await _repositorioCarpeta.Reporte(fech_inicio, fech_fin);
            return lista;

        }
        public async Task<List<CarpetaRequerimiento>> Lista()
        {
            IQueryable<CarpetaRequerimiento> query = await _repositorioCarpeta.Consultar();
            return query.Include(dv => dv.DetalleCarpeta).ToList();
        }
    }
}
