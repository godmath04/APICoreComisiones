using APICoreComisiones.Models;

namespace APICoreComisiones.Domain.Commission
{
    public interface ICommissionPolicy
    {
        decimal GetRate(decimal total, IReadOnlyCollection<Regla> reglas);
    }
}
