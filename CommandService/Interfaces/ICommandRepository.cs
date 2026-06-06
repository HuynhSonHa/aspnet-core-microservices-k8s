using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Models;

namespace CommandService.Interfaces
{
    public interface ICommandRepository
    {
        List<Command> GetAllCommandsOfPlatform(int externalPlatformId);

        Command? GetCommandOfPlatformById(int externalPlatformId, int commandId);

        void CreateCommandForPlatform(int externalPlatformId, Command command);
    }
}