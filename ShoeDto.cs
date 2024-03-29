﻿using System.ComponentModel.DataAnnotations;

namespace Firebase
{
    public class ShoeDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Brand { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public required IFormFile Video { get; set; }
    }
}
