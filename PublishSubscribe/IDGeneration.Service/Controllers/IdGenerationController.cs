using Dapr;
using IDGeneration.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IDGeneration.Service.Controllers
{

    [ApiController]
    public class IdGenerationController : ControllerBase
    {
        private readonly ILogger<IdGenerationController> _logger;

        public IdGenerationController(ILogger<IdGenerationController> logger) => _logger = logger;

        #region Moved to Client
        //[HttpPost]
        //[Route("api/idgenerator/{nodeId}")]

        //public async Task<IActionResult> RequestUniqueId(int nodeId)
        //{
        //    _logger.LogInformation($"Request to get unique Id for node {nodeId} received.");

        //    //TODO Use IoC
        //    await new MyUUIDGenerator().GenerateUniqueId(nodeId);

        //    using (var httpClient = new HttpClient())
        //    {
        //        var result = await httpClient.PostAsync(
        //             $"http://localhost:3500/v1.0/publish/IdTopic",
        //             new StringContent(JsonConvert.SerializeObject(new MyUUID { nodeId = nodeId }), Encoding.UTF8, "application/json")
        //        );

        //        _logger.LogInformation($"Unique Id request for {nodeId} published with status {result.StatusCode}!");
        //    }

        //    return Ok();
        //} 
        #endregion


        [Topic("IdTopic")]  //TODO Should be unique
        [HttpPost]
        [Route("IdTopic")]
        public async Task<IActionResult> ProcessIdGenerationRequest([FromBody] MyUUID uId)
        {
            _logger.LogInformation($"Unique Id request for node Id {uId.nodeId} processed!");
            return Ok();
        }

    }
}