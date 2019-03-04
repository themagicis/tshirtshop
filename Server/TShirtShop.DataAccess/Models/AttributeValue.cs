using System;
using System.Collections.Generic;

namespace TShirtShop.DataAccess.Models
{
    public partial class AttributeValue
    {
        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }
    }
}
