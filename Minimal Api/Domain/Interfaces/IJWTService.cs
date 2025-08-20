using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(Administrator administrator);
    }
}
