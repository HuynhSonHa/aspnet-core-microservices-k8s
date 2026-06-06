using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Platform> Platforms { get; set; }

        // //initialize the database with some data
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Platform>().HasData(
        //         new Platform() { Id = 1, Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
        //         new Platform() { Id = 2, Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
        //         new Platform() { Id = 3, Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
        //     );
        // }

    }
}