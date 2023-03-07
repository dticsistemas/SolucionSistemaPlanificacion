using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.AplicacionWeb.Utilidades.Response;
using SistemaPlanificacion.BLL.Interfaces;
using SistemaPlanificacion.Entity;

namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
   // [Authorize]
    public class CarpetaController : Controller
    {
        private readonly ITipodocumentoService _tipodocumentoService;
        private readonly ICarpetaService _capetaService;
        private readonly IMapper _mapper;


        public CarpetaController(ITipodocumentoService tipodocumento, ICarpetaService carpetaService, IMapper mapper)
        {
            _tipodocumentoService = tipodocumento;
            _capetaService = carpetaService;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Historial()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoCarpeta()
        {
            List<VMTipoDocumento> vmListaTipoDocumento = _mapper.Map<List<VMTipoDocumento>>(await _tipodocumentoService.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumento);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerPartidasPresupuestarias(string busqueda)
        {
            List<VMPartidaPresupuestaria> vmListaPartidasPresupuestarias = _mapper.Map<List<VMPartidaPresupuestaria>>(await _capetaService.ObtenerPartidasPresupuestarias(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaPartidasPresupuestarias);

        }
        [HttpPost]
        public async Task<IActionResult> RegistrarCarpeta([FromBody] VMCarpetaRequerimiento modelo)
        {
            GenericResponse<VMCarpetaRequerimiento> gResponse = new GenericResponse<VMCarpetaRequerimiento>();
            try
            {
                //modelo.IdRegional = 1;
                CarpetaRequerimiento carpeta_creada = await _capetaService.Registrar(_mapper.Map<CarpetaRequerimiento>(modelo));

                modelo = _mapper.Map<VMCarpetaRequerimiento>(carpeta_creada);
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
        [HttpGet]
        public async Task<IActionResult> Historial_Busqueda(string numeroCarpeta, string fechaInicio, string fechaFin)
        {
            List<VMCarpetaRequerimiento> vmHistorialCarpeta = _mapper.Map<List<VMCarpetaRequerimiento>>(await _capetaService.Historial(numeroCarpeta, fechaInicio, fechaFin));
            return StatusCode(StatusCodes.Status200OK, vmHistorialCarpeta);
        }

    }
}
