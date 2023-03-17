using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class CertificacionPoaService : ICertificacionPoaService
    {
        private readonly IGenericRepository<CertificacionPoa> _repositorio;

        public CertificacionPoaService(IGenericRepository<CertificacionPoa> repositorio)
        {
            _repositorio = repositorio;

        }
        async Task<CertificacionPoa> ICertificacionPoaService.Registrar(CertificacionPoa entidad)
        {
            try
            {
                CertificacionPoa certificacionPoa_creada = await _repositorio.Crear(entidad);
                if (certificacionPoa_creada.IdCertificacionPoa == 0)
                    throw new TaskCanceledException("No se pudo Registrar la Certificacion");
                return certificacionPoa_creada;
            }
            catch
            {
                throw;
            }
        }
    }
}
