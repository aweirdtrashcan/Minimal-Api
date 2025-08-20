using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Domain.DTOs
{
    public class CreateVehicleDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [StringLength(255, MinimumLength = 3)]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [Range(minimum: 1950, maximum: int.MaxValue)]
        public int Year { get; set; } = default;
    }
}
