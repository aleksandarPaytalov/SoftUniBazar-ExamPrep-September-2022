using SoftUniBazar.Data.DataConstants;
using System.ComponentModel.DataAnnotations;
using SoftUniBazar.Data.Models;

namespace SoftUniBazar.Models
{
    public class AdNewViewModel
    {
        [Required]
        [StringLength(ValidationConstants.AdNameMaxLength,
            MinimumLength = ValidationConstants.AdNameMinLength,
            ErrorMessage = ValidationConstants.LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(ValidationConstants.DescriptionMaxLength,
            MinimumLength = ValidationConstants.DescriptionMinLength,
            ErrorMessage = ValidationConstants.LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    }
}
