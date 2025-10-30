using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using APICoreComisiones.Application;
using APICoreComisiones.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICoreComisiones.Controllers
{
    [ApiController]
    [Route("api/v1/commission")]
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionService _service;

        public CommissionController(ICommissionService service)
        {
            _service = service;
        }
        [HttpPost("calculate")]
        [ProducesResponseType(typeof(IReadOnlyList<CommissionRowVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Calculate([FromBody] CalculateComissionVm vm, CancellationToken ct)
        {
            if (vm is null)
                return BadRequest("Body requerido.");

            if (!vm.FechaInicio.HasValue || !vm.FechaFin.HasValue)
                return BadRequest("Debe especificar FechaInicio y FechaFin.");

            if (vm.FechaFin < vm.FechaInicio)
                return BadRequest("FechaFin no puede ser menor que FechaInicio.");

            var rows = await _service.CalculateAsync(vm.FechaInicio.Value, vm.FechaFin.Value, ct);
            return Ok(rows);
        }
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Health() => Ok(new { status = "ok", utc = System.DateTime.UtcNow });
    }
}
