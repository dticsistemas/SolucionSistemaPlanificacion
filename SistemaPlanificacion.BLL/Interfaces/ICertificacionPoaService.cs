using SistemaPlanificacion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPlanificacion.BLL.Interfaces
{
    public interface ICertificacionPoaService
    {
        Task<CertificacionPoa> Registrar(CertificacionPoa entidad);
    }
}
