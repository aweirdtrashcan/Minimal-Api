using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Mappers
{
    public static class AdministratorMapper
    {
        public static AdministratorDTO ToAdministratorDTO(this Administrator administrator)
        {
            return new AdministratorDTO
            {
                Id = administrator.Id,
                Name = administrator.Name,
                Email = administrator.Email,
                Profile = administrator.Profile
            };
        }
    }
}
