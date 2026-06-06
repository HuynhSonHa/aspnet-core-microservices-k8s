using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Data;
using PlatformService.Interfaces;
using PlatformService.Models;

namespace PlatformService.Repositories
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

        public List<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform? GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }
    }
}