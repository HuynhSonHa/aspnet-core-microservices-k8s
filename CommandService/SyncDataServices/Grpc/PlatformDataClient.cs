using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Mappers.Platform;
using CommandService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;

        public PlatformDataClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Platform> ReceiveAllPlatforms()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);

            var client = new GrpcPlatform.GrpcPlatformClient(channel);

            var request = new GetAllRequest();

            try
            {
                var response = client.GetAllPlatforms(request);

                return response.Platforms.Select(p => p.ToPlatformFromGrpc()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not call GRPC Server: {ex.Message}");
                return null;
            }
        }
    }
}