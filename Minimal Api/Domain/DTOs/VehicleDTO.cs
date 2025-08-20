using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Domain.DTOs
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public int Year { get; set; } = default;
    }
}
