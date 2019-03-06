using System.ComponentModel.DataAnnotations;

namespace TShirtShop.Services.Attributes
{
    public class AttributeDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
