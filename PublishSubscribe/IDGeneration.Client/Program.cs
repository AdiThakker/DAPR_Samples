using IDGeneration.Common.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace IDGeneration.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DAPR!");

            //var configuration = GetConfigurationSettings();
            //var clientConfiguration = configuration.GetSection("clientConfiguration").Get<ClientConfiguration>();

            //int.TryParse(clientConfiguration.NodeId, out int nodeId);
            //if (string.IsNullOrWhiteSpace(clientConfiguration.RequestUrl))
            //    throw new InvalidOperationException("No Request url configured.");

            Console.WriteLine("Publishing ID Generation Request");

            using (var httpClient = new HttpClient())
            {
                // var result = httpClient.PostAsync($"{clientConfiguration.RequestUrl}", new StringContent(JsonConvert.SerializeObject(new IDGenerationRequest { NodeId = nodeId }), Encoding.UTF8, "application/json")).Result;
                var result = httpClient.PostAsync($"http://localhost:3500/v1.0/publish/IdGeneration", new StringContent(JsonConvert.SerializeObject(new IDGenerationRequest { NodeId = 101 }), Encoding.UTF8, "application/json")).Result;
                Console.WriteLine($"Unique Id request for 101 published with status {result.StatusCode}!");
            }

            Console.ReadKey();
        }

        private static IConfiguration GetConfigurationSettings()
        {
            var configBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return configBuilder.Build();
        }
    }
}
