using System;
using System.Collections.Generic;

namespace TShirtShop.DataAccess.Models
{
    public partial class Tax
    {
        public int TaxId { get; set; }
        public string TaxType { get; set; }
        public decimal TaxPercentage { get; set; }
    }
}
