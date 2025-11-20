using Parcial2DDA.Models;

namespace Parcial2DDA.Services

{
    public interface IbalanzaService
    {
        void RegistrarEntrada(EntradaDTO entrada);
        void RegistrarSalida(SalidaDto salida);
        decimal MaximaDiferenciaDepeso();
        decimal MaximoTiempoEnElLocal();
        int TotalMedicionesCompletadas();
    }
    }
