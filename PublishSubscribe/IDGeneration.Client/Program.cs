using IDGeneration.Common.Entities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace IDGeneration.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello DAPR!");

            Console.WriteLine("Publishing ID Generation Request");
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.PostAsync(
                     $"http://localhost:3500/v1.0/publish/IdTopic",
                     new StringContent(JsonConvert.SerializeObject(new MyUUID { nodeId = 43 }), Encoding.UTF8, "application/json")).Result;

                Console.WriteLine($"Unique Id request for {43} published with status {result.StatusCode}!");
            }



            Console.ReadKey();
        }
    }
}
