using System.ComponentModel.DataAnnotations;

namespace TShirtShop.Services.AttributeValues
{
    public class AttributeValueDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
