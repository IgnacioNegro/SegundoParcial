using Parcial2DDA.Data;
using Parcial2DDA.Models;
using System.Linq;

namespace Parcial2DDA.Services
{
    public class BalanzaService : IbalanzaService
    {
        private readonly AppDbContext _context;

        public BalanzaService(AppDbContext context)
        {
            _context = context;
        }

        private DateTime MarcarTiempo()
        {
            return DateTime.Now;
        }

        public void RegistrarEntrada(EntradaDTO entrada)
        {
            var medicionExistente = _context.Mediciones
                .FirstOrDefault(m => m.Huella == entrada.Huella && m.HoraSalida == null);

            if (medicionExistente == null)
            {
                var nueva = new Medicion
                {
                    Huella = entrada.Huella,
                    Peso = entrada.Peso,
                    HoraEntrada = MarcarTiempo()
                };

                _context.Mediciones.Add(nueva);
                _context.SaveChanges();
            }
        }

        public void RegistrarSalida(SalidaDto salida)
        {
            var medicionExistente = _context.Mediciones
                .FirstOrDefault(m => m.Huella == salida.Huella && m.HoraSalida == null);

            if (medicionExistente == null)
                return;

            medicionExistente.HoraSalida = MarcarTiempo();

            int t1 = (int)((DateTimeOffset)medicionExistente.HoraEntrada).ToUnixTimeSeconds();
            int t2 = (int)((DateTimeOffset)medicionExistente.HoraSalida).ToUnixTimeSeconds();

            medicionExistente.TiempoEnElLocal = t2 - t1;
            medicionExistente.DiferenciaPeso = salida.Peso - medicionExistente.Peso;

            _context.SaveChanges();
        }

        public decimal MaximaDiferenciaDepeso()
        {
            var lista = _context.Mediciones
                .Where(m => m.DiferenciaPeso != null)
                .OrderByDescending(m => m.DiferenciaPeso)
                .ToList();

            if (lista.Count == 0)
                return 0;

            return (decimal)lista.First().DiferenciaPeso;
        }

        public decimal MaximoTiempoEnElLocal()
        {
            var lista = _context.Mediciones
                .Where(m => m.TiempoEnElLocal != null)
                .OrderByDescending(m => m.TiempoEnElLocal)
                .ToList();

            if (lista.Count == 0)
                return 0;

            return (decimal)lista.First().TiempoEnElLocal;
        }

        public int TotalMedicionesCompletadas()
        {
            return _context.Mediciones
                .Count(m => m.HoraSalida != null);
        }
    }
}
