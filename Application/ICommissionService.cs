using APICoreComisiones.ViewModels;

namespace APICoreComisiones.Application
{
    public interface ICommissionService
    {
        Task<IReadOnlyList<CommissionRowVm>> CalculateAsync(DateTime start, DateTime end, CancellationToken ct = default);
    }
}
