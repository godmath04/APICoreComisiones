namespace APICoreComisiones.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Venta> Ventas { get; set; }
    }
}
