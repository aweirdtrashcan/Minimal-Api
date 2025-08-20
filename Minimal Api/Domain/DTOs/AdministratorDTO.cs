using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.DTOs
{
    public class AdministratorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Profile Profile { get; set; } 
    }
}
