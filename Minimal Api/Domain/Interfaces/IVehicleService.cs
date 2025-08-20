using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> All();
        Task<List<Vehicle>> All(int page, string? name, string? brand);
        Task<Vehicle?> FindById(int id);
        Task<Vehicle?> Save(Vehicle vehicle);
        Task<Vehicle?> Update(Vehicle vehicle);
        Task<Vehicle?> Delete(Vehicle vehicle);
    }
}
