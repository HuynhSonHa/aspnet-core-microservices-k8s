using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Dtos;
using CommandService.Interfaces;
using CommandService.Mappers.Command;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{externalPlatformId}/commands")]

    public class CommandController : ControllerBase
    {
        private readonly ICommandRepository _commandRepository;

        private readonly IPlatformRepository _platformRepository;

        public CommandController(ICommandRepository commandRepository, IPlatformRepository platformRepository)
        {
            _commandRepository = commandRepository;
            _platformRepository = platformRepository;
        }

        [HttpGet]
        public IActionResult GetAllCommandsForPlatform([FromRoute] int externalPlatformId)
        {
            if (!_platformRepository.ExternalPlatformExists(externalPlatformId))
            {
                return NotFound("Platform not found");
            }

            var commands = _commandRepository.GetAllCommandsOfPlatform(externalPlatformId);

            return Ok(commands.Select(c => c.ToCommandReadDto()).ToList());
        }

        [HttpGet("{commandId}")]
        public IActionResult GetCommandForPlatform([FromRoute] int externalPlatformId, [FromRoute] int commandId)
        {
            if (!_platformRepository.ExternalPlatformExists(externalPlatformId))
            {
                return NotFound("Platform not found");
            }

            var command = _commandRepository.GetCommandOfPlatformById(externalPlatformId, commandId);

            if (command == null)
            {
                return NotFound("Command not found");
            }

            return Ok(command.ToCommandReadDto());
        }

        [HttpPost]
        public IActionResult CreateCommandForPlatform([FromRoute] int externalPlatformId, CommandCreateDto commandCreateDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_platformRepository.ExternalPlatformExists(externalPlatformId))
            {
                return NotFound("Platform not found controller");
            }

            var command = commandCreateDto.ToCommand();

            _commandRepository.CreateCommandForPlatform(externalPlatformId, command);

            return CreatedAtAction(nameof(GetCommandForPlatform), new { externalPlatformId = externalPlatformId, commandId = command.Id }, command.ToCommandReadDto());


        }

    }
}