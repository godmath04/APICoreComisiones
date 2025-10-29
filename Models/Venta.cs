namespace APICoreComisiones.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Monto { get; set; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
    }
}
