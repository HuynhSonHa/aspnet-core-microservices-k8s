using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.Interfaces
{
    public interface IPlatformRepository
    {
        List<Platform> GetAllPlatforms();

        Platform? GetPlatformById(int id);

        void CreatePlatform(Platform platform);
    }
}