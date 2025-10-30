using System.ComponentModel.DataAnnotations;

namespace APICoreComisiones.ViewModels
{
    public class CalculateComissionVm
    {
        [Display(Name = "Fecha inicio")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha fin")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime? FechaFin { get; set; }
    }
}
