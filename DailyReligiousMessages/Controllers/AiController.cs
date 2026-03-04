using DailyReligiousMessages.Containers;
using DailyReligiousMessages.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DailyReligiousMessages.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AiController(IAiService aiService, IOptions<AiSettings> aiSettings) : ControllerBase
{
    //[HttpGet]
    //public async Task<IActionResult> Ask()
    //{
    //    var message = await aiService.GenerateAsync(aiSettings.Value.RamadanPrompt);
    //    return Ok(message);
    //}
}
