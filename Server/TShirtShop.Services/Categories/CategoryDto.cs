using System.ComponentModel.DataAnnotations;

namespace TShirtShop.Services.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
