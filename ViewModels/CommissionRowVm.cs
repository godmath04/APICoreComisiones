namespace APICoreComisiones.ViewModels
{
    public class CommissionRowVm
    {
        public int VendedorId { get; set; }
        public string Vendedor { get; set; } = string.Empty;
        public decimal TotalVentas { get; set; }
        public decimal PorcentajeAplicado { get; set; }
        public decimal ComisionCalculada { get; set; }
    }
}
