using Parcial2DDA.Models;

namespace Parcial2DDA.Services

{
    public interface IbalanzaService
    {
        public DateTime MarcarTiempo(); //PRONTO
        public void RegistrarEntrada(EntradaDTO entrada);// PRONTO
        public int CalcularTiempoEnElLocal(SalidaDto salida);
        public int DiferenciaDePeso(EntradaDTO entrada, SalidaDto salida);

        public void RegistrarSalida(SalidaDto salida);
        public bool MedicionCompletada(EntradaDTO entrada, SalidaDto salida);
        public decimal MaximaDiferenciaDepeso(PesoDto peso);
        public decimal MaximoTiempoEnElLocal(EntradaDTO dto);
        public int TotalMedicionesCompletadas();
    }
}
