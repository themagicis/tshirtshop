using System;
using System.Collections.Generic;

namespace TShirtShop.DataAccess.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
