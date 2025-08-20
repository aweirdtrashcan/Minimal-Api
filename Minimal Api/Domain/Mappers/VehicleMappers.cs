using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Mappers
{
    public static class VehicleMappers
    {
        public static VehicleDTO ToVehicleDTO(this Vehicle vehicle)
        {
            return new VehicleDTO
            {
                Id = vehicle.Id,
                Name = vehicle.Name,
                Brand = vehicle.Brand,
                Year = vehicle.Year
            };
        }
    }
}
