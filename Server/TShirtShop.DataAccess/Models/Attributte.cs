using System.Collections.Generic;

namespace TShirtShop.DataAccess.Models
{
    public partial class Attributte
    {
        public int AttributeId { get; set; }
        public string Name { get; set; }

        public ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
    }
}
