using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Models;
using Parcial2DDA.Services;

namespace Parcial2DDA.Controllers
{
    [ApiController]
    [Route("/")]
    public class BalanzaController : ControllerBase
    {
        private readonly IbalanzaService _service;

        public BalanzaController(IbalanzaService service)
        {
            _service = service;
        }

        [HttpPost("medicion")]
        public IActionResult Entrada([FromBody] EntradaDTO dto)
        {
            _service.RegistrarEntrada(dto);
            return Ok(new
            {
                huella = dto.Huella,
                peso = dto.Peso,
                tipo = "entrada"
            });
        }

        [HttpPost("medicion/salida")]
        public IActionResult Salida([FromBody] SalidaDto dto)
        {
            _service.RegistrarSalida(dto);
            return Ok(new
            {
                huella = dto.Huella,
                peso = dto.Peso,
                tipo = "salida"
            });
        }

        [HttpGet("reportes/total")]
        public IActionResult TotalMedicionesCompletadas()
        {
            return Ok(new
            {
                total_mediciones_completadas = _service.TotalMedicionesCompletadas()
            });
        }

        [HttpGet("reportes/maxima_diferencia_peso")]
        public IActionResult MaximaDiferenciaDePeso()
        {
            return Ok(new
            {
                maxima_diferencia_peso = _service.MaximaDiferenciaDepeso()
            });
        }

        [HttpGet("reportes/maximo_tiempo")]
        public IActionResult MaximoTiempoEnElLocal()
        {
            return Ok(new
            {
                maximo_tiempo = _service.MaximoTiempoEnElLocal()
            });
        }
    }
}
