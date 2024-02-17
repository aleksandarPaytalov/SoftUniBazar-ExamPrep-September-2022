using SoftUniBazar.Data.DataConstants;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.CategoryNameMaxLength,
            MinimumLength = ValidationConstants.CategoryNameMinLength,
            ErrorMessage = ValidationConstants.LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
