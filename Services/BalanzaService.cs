
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Parcial2DDA.Data;
using Parcial2DDA.Models;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace Parcial2DDA.Services
{
    public class BalanzaService:IbalanzaService
    {
        private readonly AppDbContext _context;

        public BalanzaService(AppDbContext context)
        {
            _context = context;
        }


        public DateTime MarcarTiempo()
        {

            return DateTime.Now;
        }
        public void RegistrarEntrada (EntradaDTO entrada)
        {
            var MedicionExistente= _context.Mediciones.FirstOrDefault(m => m.Huella == entrada.Huella);
            if (MedicionExistente == null)
            {
                Medicion nueva = new Medicion
                {
                    Huella = entrada.Huella,
                    Peso = entrada.Peso,
                    HoraEntrada = MarcarTiempo(),
                    HoraSalida = MarcarTiempo()

                };

                    _context.Mediciones.Add(nueva);
                }

           
            _context.SaveChanges();
        }

        public void RegistrarSalida (SalidaDto salida) {
        {
                var medicionExistente = _context.Mediciones.FirstOrDefault(m => m.Huella == salida.Huella);
                var EntradaDto = new EntradaDTO(); decimal PesoEntrada= EntradaDto.Peso;
                var SalidaDto = new SalidaDto(); decimal PesoSalida= SalidaDto.Peso;
                var MedicionExistente = _context.Mediciones.FirstOrDefault(m => m.Huella == salida.Huella);

                new Medicion {
                    Huella = salida.Huella,
                    Peso = salida.Peso,
                    TiempoEnElLocal = CalcularTiempoEnElLocal(salida),
                };
               

                DiferenciaDePeso (EntradaDto,SalidaDto);

                _context.SaveChanges();


            }
        }



        public int CalcularTiempoEnElLocal (SalidaDto salida)
        {
        
            
            var MedicionExistente= _context.Mediciones.FirstOrDefault(m => m.Huella == salida.Huella);
            int ts1 = (int)((DateTimeOffset)MedicionExistente.HoraEntrada).ToUnixTimeSeconds();
            int ts2 = (int)((DateTimeOffset)MedicionExistente.HoraSalida).ToUnixTimeSeconds();
         
            int diferencia= ts2 - ts1;

            MedicionExistente.TiempoEnElLocal = diferencia;

            _context.SaveChanges();
            return diferencia;
        }

        public int DiferenciaDePeso(EntradaDTO entrada, SalidaDto salida)
        {
            if (entrada.Huella != salida.Huella)
            {
                throw new  Exception("Las huellas no coinciden");
            }
            int diferenciaPeso = (int)(entrada.Peso - salida.Peso);
            return diferenciaPeso;
        } 

        public bool MedicionCompletada (EntradaDTO entrada, SalidaDto salida)
        {
            var MedicionExistente= _context.Mediciones.FirstOrDefault(m => m.Huella == entrada.Huella);
            if (entrada.Huella != salida.Huella)
            {
                throw new Exception("Las huellas no coinciden");
                
            }

            if (entrada.Huella == salida.Huella)
            {
              
                _context.Mediciones.Remove(MedicionExistente);
                
            }
                      
            return true; 

            
        }


        public decimal MaximaDiferenciaDepeso (PesoDto peso)
        {
            var MedicionExistente= _context.Mediciones.FirstOrDefault(m => m.Peso == peso.MaximaDiferenciaDePeso);

            return peso.MaximaDiferenciaDePeso;
        }
        
        public decimal MaximoTiempoEnElLocal(EntradaDTO dto)
        {
            var MedicionExistente= _context.Mediciones.FirstOrDefault(m => m.Huella == dto.Huella);
            if (dto.Huella != dto.Huella)
            {
                throw new Exception("Las huellas no coinciden");
            }

            var TiempoMaximoEnEllocal= _context.Mediciones.Max(m => m.TiempoEnElLocal);

            decimal TiempoMaximoEnEllocalValue = TiempoMaximoEnEllocal.HasValue ? TiempoMaximoEnEllocal.Value : 0;

            return TiempoMaximoEnEllocalValue;

        }

        public int TotalMedicionesCompletadas ()
        {
            int totalMediciones= _context.Mediciones.Count();
            return totalMediciones;
        }
    }
}
