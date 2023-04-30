using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PlatformService.Models.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_configuration.GetValue<string>("CommandService") + "/api/c/platforms/", httpContent);
            response.EnsureSuccessStatusCode();

            Console.WriteLine("Sync POST to SendPlatformToCommand was ok");
        }
    }
}
