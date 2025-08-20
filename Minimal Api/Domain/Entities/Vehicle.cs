using MinimalApi.Domain.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Domain.Entities
{
    public class Vehicle
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default;
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string Brand { get; set; } = string.Empty;
        [Required]
        [Column(name:"YearMade")]
        public int Year { get; set; } = default!;
    }
}
