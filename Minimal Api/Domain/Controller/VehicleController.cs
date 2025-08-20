using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.Mappers;

namespace MinimalApi.Domain.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.All();
            return Ok(vehicles.Select(v => v.ToVehicleDTO()));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<IActionResult> PostVehicle([FromBody] CreateVehicleDTO createVehicleDTO)
        {
            var vehicle = new Vehicle
            {
                Brand = createVehicleDTO.Brand,
                Name = createVehicleDTO.Name,
                Year = createVehicleDTO.Year
            };

            var entity = await _vehicleService.Save(vehicle);

            if (entity == null)
            {
                return BadRequest();
            }
            
            return Ok(entity.ToVehicleDTO());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteFromId([FromRoute] int id)
        {
            var entity = await _vehicleService.FindById(id);
            if (entity == null)
            {
                return NotFound($"Entity with id {id} does not exist");
            }

            await _vehicleService.Delete(entity);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateVehicleDTO updateVehicleDTO)
        {
            var vehicle = new Vehicle
            {
                Id = id,
                Brand = updateVehicleDTO.Brand,
                Name = updateVehicleDTO.Name,
                Year = updateVehicleDTO.Year
            };
            
            var entity = await _vehicleService.Update(vehicle);
            if (entity == null)
            {
                return BadRequest();
            }

            return Ok(entity.ToVehicleDTO());
        }
    }
}
