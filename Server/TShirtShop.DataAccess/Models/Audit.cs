using System;
using System.Collections.Generic;

namespace TShirtShop.DataAccess.Models
{
    public partial class Audit
    {
        public int AuditId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
