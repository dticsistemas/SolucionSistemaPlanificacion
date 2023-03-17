using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
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
        private readonly ICarpetaService _carpetaService;
        private readonly IUnidadresponsableService _unidadResponsableServicio;
        private readonly IProgramaService _programaServicio;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;


        public CarpetaController(ITipodocumentoService tipodocumento, ICarpetaService carpetaService, IUnidadresponsableService unidadResponsableServicio, IProgramaService programaServicio, IMapper mapper, IConverter converter)
        {
            _tipodocumentoService = tipodocumento;
            _carpetaService = carpetaService;
            _mapper = mapper;
            _converter = converter;
            _unidadResponsableServicio = unidadResponsableServicio;
            _programaServicio = programaServicio;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Historial()
        {
            return View();
        }       
        public IActionResult ListadoCarpeta()
        {
            return View();
        }
        public IActionResult ListadoPlanificacion()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CertificarPlanificacionAsync(string numeroCarpeta)
        {
            numeroCarpeta = "000055";
            VMCarpetaRequerimiento vmCarpeta = _mapper.Map<VMCarpetaRequerimiento>(await _carpetaService.Detalle(numeroCarpeta));
            VMPDFCarpeta modelo = new VMPDFCarpeta();
            modelo.carpeta = vmCarpeta;

            return View(modelo);
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
            List<VMPartidaPresupuestaria> vmListaPartidasPresupuestarias = _mapper.Map<List<VMPartidaPresupuestaria>>(await _carpetaService.ObtenerPartidasPresupuestarias(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaPartidasPresupuestarias);

        }
        [HttpPost]
        public async Task<IActionResult> RegistrarCarpeta([FromBody] VMCarpetaRequerimiento modelo)
        {
            GenericResponse<VMCarpetaRequerimiento> gResponse = new GenericResponse<VMCarpetaRequerimiento>();
            try
            {
                //modelo.IdRegional = 1;
                CarpetaRequerimiento carpeta_creada = await _carpetaService.Registrar(_mapper.Map<CarpetaRequerimiento>(modelo));

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
            if(fechaInicio!=null)
                fechaInicio = fechaInicio.Trim();
            if(fechaFin!=null)
                fechaFin = fechaFin.Trim();
            var service = await _carpetaService.Historial(numeroCarpeta, fechaInicio, fechaFin);
            List<VMCarpetaRequerimiento> vmHistorialCarpeta = _mapper.Map<List<VMCarpetaRequerimiento>>(service);
            return StatusCode(StatusCodes.Status200OK, vmHistorialCarpeta);
        }

        public IActionResult MostrarPDFCarpeta(string numeroCarpeta)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFCarpeta?numeroCarpeta={numeroCarpeta}";
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page = urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);
            return File(archivoPDF, "application/pdf");
        }

        [HttpGet]
        public async Task<IActionResult> ListaUnidadresponsable()
        {
            List<VMUnidadResponsable> vmListaUnidadesResponsables = _mapper.Map<List<VMUnidadResponsable>>(await _unidadResponsableServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaUnidadesResponsables);
        }
        [HttpGet]
        public async Task<IActionResult> ListaPrograma()
        {
            List<VMPrograma> vmListaProgramas = _mapper.Map<List<VMPrograma>>(await _programaServicio.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaProgramas);
        }
        [HttpGet]
        public async Task<IActionResult> ListaMisCarpetas()
        {
            List<VMCarpetaRequerimiento> vmListaCarpetas = _mapper.Map<List<VMCarpetaRequerimiento>>(await _carpetaService.Lista());           
            return StatusCode(StatusCodes.Status200OK, new { data = vmListaCarpetas });
        }


    }
}
