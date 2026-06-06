using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string  Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Publisher cannot exceed 100 characters.")]
        public string Publisher { get; set; } = string.Empty;

        [Required]
        public string Cost { get; set; } = string.Empty;
    }
}