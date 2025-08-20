using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DbContexto _dbContext;
        private readonly int _pageSize = 5;

        public VehicleService(DbContexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Vehicle>> All()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<List<Vehicle>> All(int page, string? name, string? brand)
        {
            int skipCount = _pageSize * (page - 1);
            var query = _dbContext.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(v => name.Contains(v.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(v => brand.Contains(v.Brand, StringComparison.OrdinalIgnoreCase));
            }

            var vehicles = await query
                .Skip(skipCount)
                .Take(_pageSize)
                .ToListAsync();

            return vehicles;
        }

        public async Task<Vehicle?> FindById(int id)
        {
            return await _dbContext.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle?> Save(Vehicle vehicle)
        {
            var savedVehicle = await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();

            return savedVehicle.Entity;
        }

        public async Task<Vehicle?> Update(Vehicle vehicle)
        {
            var entity = await FindById(vehicle.Id);
            if (entity != null)
            {
                entity.Brand = vehicle.Brand;
                entity.Name = vehicle.Name;
                entity.Year = vehicle.Year;

                await _dbContext.SaveChangesAsync();
                return entity;
            }

            return null;
        }

        public async Task<Vehicle?> Delete(Vehicle vehicle)
        {
            var entity = _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
