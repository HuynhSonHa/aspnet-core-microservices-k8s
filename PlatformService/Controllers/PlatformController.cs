using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Dtos;
using PlatformService.Interfaces;
using PlatformService.Mappers;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{

    [Route("api/platform")]
    [ApiController]


    public class PlatformController : ControllerBase
    {

        private readonly IPlatformRepository _platformRepository;

        private readonly ICommandDataClient _commandDataClient;

        private readonly IMessageBusClient _messageBusClient;

        public PlatformController(IPlatformRepository repository, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
        {
            _platformRepository = repository;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public IActionResult GetAllPlatforms()
        {
            var platforms = _platformRepository.GetAllPlatforms();
            return Ok(platforms.Select(p => p.ToPlatformReadDto()));
        }
        

        [HttpGet("{id}")]
        public IActionResult GetPlatformById([FromRoute] int id)
        {
            var platform = _platformRepository.GetPlatformById(id);
            if (platform == null)
            {
                return NotFound();
            }
            return Ok(platform.ToPlatformReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatformAsync([FromBody] PlatformCreateDto platformCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var platform = platformCreateDto.ToPlatformFromCreateDto();

            _platformRepository.CreatePlatform(platform);

            // Sync with Command Service by sending HTTP request
            try
            {
                await _commandDataClient.SendPlatformToCommandService(platform.ToPlatformReadDto());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not sync with Command Service by HTTP: {ex.Message}");
            }

            // Async with Command Service by sending message to RabbitMQ
            try
            {
                var platformPublishDto = platform.ToPlatformReadDto().ToPlatformPublishDto();

                platformPublishDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not sync with Command Service by RabbitMQ: {ex.Message}");
            }


            return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platform.ToPlatformReadDto());
        }

    }
}