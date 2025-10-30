using Microsoft.EntityFrameworkCore;
using APICoreComisiones.Data;
using APICoreComisiones.Domain.Commission;
using APICoreComisiones.ViewModels;

namespace APICoreComisiones.Application
{
    public class CommissionService : ICommissionService
    {
        private readonly AppDbContext _db;
        private readonly ICommissionPolicy _policy;

        public CommissionService(AppDbContext db, ICommissionPolicy policy)
        {
            _db = db;
            _policy = policy;
        }

        public async Task<IReadOnlyList<CommissionRowVm>> CalculateAsync(DateTime start, DateTime end, CancellationToken ct = default)
        {
            if (end < start) throw new ArgumentException("Fecha fin no puede ser menor que fecha inicio");

            var totales = await _db.Ventas
                .AsNoTracking()
                .Where(v => v.FechaVenta >= start && v.FechaVenta <= end)
                .GroupBy(v => new { v.VendedorId, v.Vendedor.Nombre })
                .Select(g => new
                {
                    g.Key.VendedorId,
                    Nombre = g.Key.Nombre,
                    Total = g.Sum(x => x.Monto)
                })
                .ToListAsync(ct);

            var reglas = await _db.Reglas
                .AsNoTracking()
                .OrderBy(r => r.MontoMinimo)
                .ToListAsync(ct);

            var filas = new List<CommissionRowVm>(totales.Count);
            foreach (var t in totales)
            {
                var rate = _policy.GetRate(t.Total, reglas);
                filas.Add(new CommissionRowVm
                {
                    VendedorId = t.VendedorId,
                    Vendedor = t.Nombre,
                    TotalVentas = t.Total,
                    PorcentajeAplicado = rate,
                    ComisionCalculada = Math.Round(t.Total * rate, 2, MidpointRounding.ToEven)
                });
            }
            return filas;
        }

    }
}
