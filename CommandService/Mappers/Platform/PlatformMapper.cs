using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Dtos;
using CommandService.Dtos.Platform;
using CommandService.Mappers.Command;
using CommandService.Models;
using PlatformService;

namespace CommandService.Mappers.Platform
{
    public static class PlatformMapper
    {
        public static PlatformReadDto ToPlatformReadDto(this Models.Platform platform)
        {
            return new PlatformReadDto
            {
                Id = platform.ExternalId,
                Name = platform.Name,
                Commands = platform.Commands.Select(c => c.ToCommandReadDto()).ToList()
            };
        }

        public static Models.Platform ToPlatform(this PlatformPublishDto platformPublishDto)
        {
            return new Models.Platform
            {
                ExternalId = platformPublishDto.Id,
                Name = platformPublishDto.Name
            };
        }

        public static Models.Platform ToPlatformFromGrpc(this GrpcPlatformModel grpcPlatformModel)
        {
            return new Models.Platform
            {
                ExternalId = grpcPlatformModel.PlatformId,
                Name = grpcPlatformModel.Name
            };
        }
    }
}