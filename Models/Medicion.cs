namespace Parcial2DDA.Models
{
    public class Medicion
    {
        public int id { get; set; }

        public string Huella { get; set; }
        public decimal Peso { get; set; }

        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }

        public decimal? TiempoEnElLocal { get; set; }

        public decimal? DiferenciaPeso { get; set; }

        public Medicion() { }
    }
}