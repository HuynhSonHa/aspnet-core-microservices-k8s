using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Mappers.Command
{
    public static class CommandMapper
    {
        public static CommandReadDto ToCommandReadDto(this Models.Command command)
        {
            return new CommandReadDto
            {
                Id = command.Id,
                HowTo = command.HowTo,
                CommandLine = command.CommandLine,
                PlatformId = command.Platform.ExternalId
            };
        }

        public static Models.Command ToCommand(this CommandCreateDto commandCreateDto)
        {
            return new Models.Command
            {
                HowTo = commandCreateDto.HowTo,
                CommandLine = commandCreateDto.CommandLine
            };
        }
    }
}