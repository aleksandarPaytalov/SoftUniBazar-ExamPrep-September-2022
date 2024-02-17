using SoftUniBazar.Data.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models
{
    public class AdViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.AdNameMaxLength,
            MinimumLength = ValidationConstants.AdNameMinLength,
            ErrorMessage = ValidationConstants.LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string CreatedOn { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(ValidationConstants.DescriptionMaxLength, 
            MinimumLength = ValidationConstants.DescriptionMinLength,
            ErrorMessage = ValidationConstants.LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Owner { get; set; } = null!;
    }
}
