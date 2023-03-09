using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SistemaPlanificacion.AplicacionWeb.Models.ViewModels;
using SistemaPlanificacion.BLL.Interfaces;


namespace SistemaPlanificacion.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        //private readonly INegocioService _negocioServicio;
        private readonly ICarpetaService _carpetaServicio;

        public PlantillaController(IMapper mapper, ICarpetaService carpetaServicio)
        {
            _mapper = mapper;
            _carpetaServicio = carpetaServicio;
            //_negocioServicio = negocioServicio;
        }
        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";
            return View();
        }
        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;
            return View();
        }
        public async Task<IActionResult> PDFCarpeta(string numeroCarpeta)
        {
            VMCarpetaRequerimiento vmCarpeta = _mapper.Map<VMCarpetaRequerimiento>(await _carpetaServicio.Detalle(numeroCarpeta));
           // VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioServicio.Obtener());
            VMPDFCarpeta modelo = new VMPDFCarpeta();
           
           // modelo.negocio = vmNegocio;
            modelo.carpeta=vmCarpeta;

            return View(modelo);
        }
    }
}
