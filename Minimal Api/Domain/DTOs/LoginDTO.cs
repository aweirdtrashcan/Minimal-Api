using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Domain.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Senha { get; set; } = string.Empty;
    }
}
