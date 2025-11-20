using Microsoft.AspNetCore.Mvc;
using Parcial2DDA.Data;
using Parcial2DDA.Models;
using Parcial2DDA.Services;
using System.Text.RegularExpressions;

namespace Parcial2DDA.Controllers
{
    [ApiController]
    [Route("/")]
    public class BalanzaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IbalanzaService _service;

        public BalanzaController(AppDbContext context, IbalanzaService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("/medicion")]
        public IActionResult Entrada ([FromBody] EntradaDTO dto)
        {
            
            _service.RegistrarEntrada(dto);

            return Ok(new Medicion
            {
                Huella = dto.Huella,
                Peso = dto.Peso,
                
                });

        }




        [HttpPost]
        public IActionResult Salida([FromBody] SalidaDto dto)
        {
            _service.RegistrarSalida(dto);
            
            return Ok(new Medicion
            {
                Huella = dto.Huella,
                Peso = dto.Peso,
                
            });

        }

        [HttpGet("reportes/maxima_diferencia_peso")]

        public IActionResult MaximaDiferenciaDePeso([FromBody] PesoDto dto)
        {
            var resultado = _service.MaximaDiferenciaDepeso (dto);
            return Ok(resultado);
        }

        [HttpGet("reportes/tiempo_maximo_local")]
        public IActionResult MaximoTiempoEnElLocal([FromBody] EntradaDTO dto)
        {
            var resultado = _service.MaximoTiempoEnElLocal(dto);
            return Ok(resultado);
        }

        [HttpGet("reportes/total")]

        public IActionResult TotalMedicionesCompletadas()
        {
            var resultado = _service.TotalMedicionesCompletadas();
            return Ok(resultado);
        }



    }
}
