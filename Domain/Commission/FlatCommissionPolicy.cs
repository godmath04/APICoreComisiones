using APICoreComisiones.Models;

namespace APICoreComisiones.Domain.Commission
{
    public class FlatCommissionPolicy : ICommissionPolicy
    {
        public decimal GetRate(decimal total, IReadOnlyCollection<Regla> reglas)
        {
            if (reglas == null || reglas.Count == 0) return 0m;

            var rule = reglas
                .Where(r => r.MontoMinimo <= total)
                .OrderByDescending(r => r.MontoMinimo)
                .FirstOrDefault();

            return rule?.Porcentaje ?? 0m;
        }
    }
}
