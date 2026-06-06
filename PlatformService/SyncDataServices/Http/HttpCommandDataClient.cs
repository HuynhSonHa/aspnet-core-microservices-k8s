using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {

        private readonly HttpClient _httpClient;

        private readonly IConfiguration configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task SendPlatformToCommandService(PlatformReadDto platformDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platformDto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(configuration["CommandService"], httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}