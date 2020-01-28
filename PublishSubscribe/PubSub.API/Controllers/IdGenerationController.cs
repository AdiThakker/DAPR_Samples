﻿using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PubSub.Domain;
using PubSub.Domain.Entities;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PubSub.API.Controllers
{

    [ApiController]
    public class IdGenerationController : ControllerBase
    {
        private readonly ILogger<IdGenerationController> _logger;

        public IdGenerationController(ILogger<IdGenerationController> logger) => _logger = logger;

        [HttpPost]
        [Route("api/idgenerator/{nodeId}")]

        public async Task<IActionResult> RequestUniqueId(int nodeId)
        {
            _logger.LogInformation($"Request to get unique Id for node {nodeId} received.");

            //TODO Use IoC
            await new MyUUIDGenerator().GenerateUniqueId(nodeId);

            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync(
                     $"http://localhost:3500/v1.0/publish/IdTopic",
                     new StringContent(JsonConvert.SerializeObject(new MyUUID { nodeId = nodeId }), Encoding.UTF8, "application/json")
                );

                _logger.LogInformation($"Unique Id request for {nodeId} published with status {result.StatusCode}!");
            }

            return Ok();
        }


        [Topic("IdTopic")]  //TODO Should be unique
        [HttpPost]
        [Route("IdTopic")]
        public async Task<IActionResult> ProcessOrder([FromBody] MyUUID uId)
        {
            _logger.LogInformation($"Unique Id request for node Id {uId.nodeId} processed!");
            return Ok();
        }

    }
}