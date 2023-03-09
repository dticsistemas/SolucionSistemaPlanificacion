using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.DAL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.BLL.Implementacion
{
    public class NegocioService : INegocioService
    {
        private readonly IGenericRepository<Negocio> _repositorio;
        public NegocioService(IGenericRepository<Negocio> repositorio)
        {
            _repositorio = repositorio;
        }
        public Task<Negocio> GuardarCambios(Negocio entidad, Stream logo = null, string Nombrelogo = "")
        {
            throw new NotImplementedException();
        }

        public Task<Negocio> Obtener()
        {
            throw new NotImplementedException();
        }
    }
}
