using Microsoft.AspNetCore.Mvc;
using ResumeGenerator.Interfaces;
using ResumeGenerator.Models;
using ResumeGenerator.Services;

namespace ResumeGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController :ControllerBase
    {
        private readonly IOpenAiService _openAiService;   
        public ResumeController(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] ResumeRequest request)
        {
            var result = await _openAiService.GenerateResumeAsync(request);
            return Ok(new { resume = result });
        }
    }
}
