using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Infra.Interfaces
{
    public interface IAdministratorService
    {
        Task<Administrator?> Login(LoginDTO loginDTO);
        Task<Administrator?> Register(RegisterDTO registerDTO);
        Task<Administrator?> FindById(int id);
        Task<List<Administrator>> All(int? page, int? pageSize);
    }
}
