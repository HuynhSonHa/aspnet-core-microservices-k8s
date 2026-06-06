using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Interfaces;
using CommandService.Mappers.Platform;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/platform")]

    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;

        public PlatformController(IPlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
        }

        [HttpGet]
        public IActionResult GetAllPlatforms()
        {
            var platforms = _platformRepository.GetAllPlatforms();
            return Ok(platforms.Select(p => p.ToPlatformReadDto()).ToList());
        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("Inbound POST # Command Service");
            return Ok("Inbound test of from Platforms Controller");
        }
    }
}