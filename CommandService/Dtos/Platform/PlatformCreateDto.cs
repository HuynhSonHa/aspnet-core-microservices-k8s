using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Dtos.Platform
{
    public class PlatformCreateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}