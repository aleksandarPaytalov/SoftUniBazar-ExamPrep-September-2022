using System.ComponentModel.DataAnnotations;
using SoftUniBazar.Data.DataConstants;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}
