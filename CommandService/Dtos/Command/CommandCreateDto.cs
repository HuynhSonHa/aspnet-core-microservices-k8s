using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        [MaxLength(250, ErrorMessage = "HowTo must be less than 250 characters.")]
        public string HowTo { get; set; } = string.Empty;

        [Required]
        [MaxLength(250, ErrorMessage = "CommandLine must be less than 250 characters.")]
        public string CommandLine { get; set; } = string.Empty;
        
    }
}