using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Interfaces;
using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = scope.ServiceProvider.GetRequiredService<IPlatformDataClient>();

                var platforms = grpcClient.ReceiveAllPlatforms();

                SeedData(scope.ServiceProvider.GetRequiredService<IPlatformRepository>(), platforms);
            }
        }

        private static void SeedData(IPlatformRepository repository, List<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platforms...");

            if (platforms == null || !platforms.Any())
            {
                Console.WriteLine("No platforms received from gRPC");
                return;
            }
            
            foreach (var platform in platforms)
            {
                if (!repository.ExternalPlatformExists(platform.ExternalId))
                {
                    repository.CreatePlatform(platform);
                }
            }
        }
    }
}