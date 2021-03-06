﻿using System;
using System.Collections.Generic;

namespace TShirtShop.DataAccess.Models
{
    public partial class ShoppingCart
    {
        public int ItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public string Attributes { get; set; }
        public int Quantity { get; set; }
        public bool? BuyNow { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
