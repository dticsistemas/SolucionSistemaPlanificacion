using SistemaPlanificacion.DAL.DBContext;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.DAL.Implementacion
{
    public class CarpetaRequerimientoRepository : GenericRepository<CarpetaRequerimiento>, ICarpetaRequerimientoRepository
    {
        private readonly BasePlanificacionContext _dbContext;
        public CarpetaRequerimientoRepository(BasePlanificacionContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CarpetaRequerimiento> Registrar(CarpetaRequerimiento entidad)
        {
            CarpetaRequerimiento carpetaGenerada = new CarpetaRequerimiento();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // foreach (DetalleCarpetum dv in entidad.DetalleCarpeta)
                    // {
                    //DetalleCarpetum producto_encontrado = _dbContext.  manejo de stock cantidad de productos
                    // dbContect .Update
                    // }
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "carpeta").First();
                    correlativo.UltimonroCorrelativo = correlativo.UltimonroCorrelativo + 1;
                    correlativo.FechaActualizacion = DateTime.Now;
                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();
                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numeroCarpeta = ceros + correlativo.UltimonroCorrelativo.ToString();
                    numeroCarpeta = numeroCarpeta.Substring(numeroCarpeta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);
                    entidad.NumeroCarpeta =numeroCarpeta;
                    await _dbContext.CarpetaRequerimientos.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();
                    carpetaGenerada = entidad;
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return carpetaGenerada;
        }

        public async Task<List<DetalleCarpetum>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            //List<DetalleCarpetum> listaResumen = await _dbContext.DetalleCarpetums
            // .Include(v => v.IdCarpetaNavigation)
            //.ThenInclude(u=>u.)
            //.Include(v=>v.IdCarpetaNavigation)
            //
            //  .Where(dv => dv.IdCarpetaNavigation.FechaCreated.Value.Date >= FechaInicio.Date &&
            //  dv.IdCarpetaNavigation.FechaCreated <= FechaFin.Date).ToListAsync();
            // return listaResumen;
            throw new NotImplementedException();
        }
    }
}
