using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Data;
using CommandService.Interfaces;
using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public void CreatePlatform(Platform platform)
        {
            _context.Platforms.Add(platform);
            _context.SaveChanges();
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            if (_context.Platforms.Any(p => p.ExternalId == externalPlatformId))
            {
                return true;
            }
            return false;
        }

        public List<Platform> GetAllPlatforms()
        {
            return _context.Platforms.Include(p => p.Commands).ToList();
        }

        public bool PlatformExists(int platformId)
        {
            if (_context.Platforms.Any(p => p.Id == platformId))
            {
                return true;
            }
            return false;

        }
    }
}