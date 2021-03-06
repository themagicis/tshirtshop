﻿using System.ComponentModel.DataAnnotations;

namespace TShirtShop.Services.Departments
{
    public class DepartmentDto
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
