using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class CertificacionPoaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICertificacionPoaService _certificacionPoaService;

        public CertificacionPoaController(IMapper mapper, ICertificacionPoaService certificacionPoaService)
        {
            _mapper = mapper;
            _certificacionPoaService = certificacionPoaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VMCertificacionPoa modelo)
        {
            GenericResponse<VMCertificacionPoa> gResponse = new GenericResponse<VMCertificacionPoa>();
            try
            {
                CertificacionPoa certificacionPoa_creada = await _certificacionPoaService.Registrar(_mapper.Map<CertificacionPoa>(modelo));
                modelo = _mapper.Map<VMCertificacionPoa>(certificacionPoa_creada);
                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }
    }
}
