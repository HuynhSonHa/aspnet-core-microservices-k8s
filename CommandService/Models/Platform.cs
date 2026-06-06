using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; } // Id (primary key) from PlatformService

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Command> Commands { get; set; } = new List<Command>();
    }
}