using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Mappers
{
    public static class PlatformMapper
    {
        public static PlatformReadDto ToPlatformReadDto(this Platform platform)
        {
            return new PlatformReadDto()
            {
                Id = platform.Id,
                Name = platform.Name,
                Publisher = platform.Publisher,
                Cost = platform.Cost
            };
        }

        public static Platform ToPlatformFromCreateDto(this PlatformCreateDto platformCreateDto)
        {
            return new Platform()
            {
                Name = platformCreateDto.Name,
                Publisher = platformCreateDto.Publisher,
                Cost = platformCreateDto.Cost
            };
        }

        public static PlatformPublishDto ToPlatformPublishDto(this PlatformReadDto platformreadDto)
        {
            return new PlatformPublishDto()
            {
                Id = platformreadDto.Id,
                Name = platformreadDto.Name
            };
        }

        public static GrpcPlatformModel ToGrpcPlatformModel(this Platform platform)
        {
            return new GrpcPlatformModel()
            {
                PlatformId = platform.Id,
                Name = platform.Name,
                Publisher = platform.Publisher
            };
        }
    }
}