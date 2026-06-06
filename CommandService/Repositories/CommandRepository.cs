using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Data;
using CommandService.Interfaces;
using CommandService.Models;

namespace CommandService.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context;
        }


        public void CreateCommandForPlatform(int externalPlatformId, Command command)
        {
            var platform = _context.Platforms.Where(p => p.ExternalId == externalPlatformId).FirstOrDefault();

            if (platform == null)
            {
                throw new Exception("Platform not found repo");
            }

            command.PlatformId = platform.Id;

            _context.Commands.Add(command);
            _context.SaveChanges();
        }

        public List<Command> GetAllCommandsOfPlatform(int externalPlatformId)
        {
            var platform = _context.Platforms.Where(p => p.ExternalId == externalPlatformId).FirstOrDefault();

            // No need to check if platform is null because it will be checked in controller

            return _context.Commands.Where(c => c.PlatformId == platform.Id).OrderBy(c => c.Platform.Name).ToList();
        }

        public Command? GetCommandOfPlatformById(int externalPlatformId, int commandId)
        {
            var platform = _context.Platforms.Where(p => p.ExternalId == externalPlatformId).FirstOrDefault();

            // No need to check if platform is null because it will be checked in controller

            if (!_context.Commands.Any(p => p.Id == commandId))
            {
                return null;
            }

            return _context.Commands.Where(c => c.PlatformId == platform.Id && c.Id == commandId).FirstOrDefault();
        }
    }
}