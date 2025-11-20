namespace Parcial2DDA.Models
{

        public class EntradaDTO
    {
        public string Huella { get; set; }
        public decimal Peso { get; set; }
        public string Tipo { get; set; } = "Entrada";

    

    }


    public class SalidaDto
    {
        public string Huella { get; set; }
        public decimal Peso { get; set; }
        public string Tipo { get; set; } = "Salida";

 


    }

    public class PesoDto
    {
     public decimal MaximaDiferenciaDePeso { get; set; }
    }

    public class TiempoDto
    {
        public int Maximo_Tiempo { get; set; }
    }
}

