using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Models;

namespace CommandService.Interfaces
{
    public interface IPlatformRepository
    {
        List<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);

        bool PlatformExists(int platformId);

        bool ExternalPlatformExists(int externalPlatformId);
        

    }
}