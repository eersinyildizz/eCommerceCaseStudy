using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HepsiBuradaCaseStudy.Business.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace HepsiBuradaCaseStudy.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ScenarioFileController : ControllerBase
    {
        private readonly IScenarioFileService scenarioFileService;

        public ScenarioFileController(IScenarioFileService scenarioFileService)
        {
            this.scenarioFileService = scenarioFileService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> ReadScenarioFile()
        {
            var res = await scenarioFileService.ReadFileAsync();
            return Ok(res);
        }
    }
}
