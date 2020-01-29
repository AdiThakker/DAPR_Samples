using Dapr;
using IDGeneration.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IDGeneration.Service.Controllers
{

    [ApiController]
    public class IDGenerationController : ControllerBase
    {
        private readonly ILogger<IDGenerationController> _logger;

        public IDGenerationController(ILogger<IDGenerationController> logger) => _logger = logger;

        [Topic("IDGeneration")]  //TODO Should be unique
        [HttpPost]
        [Route("IdTopic")]
        public async Task<IActionResult> ProcessIdGenerationRequest([FromBody] IDGenerationRequest uId)
        {
            _logger.LogInformation($"Unique Id request for node Id {uId.NodeId} processed!");
            return Ok();
        }

    }
}