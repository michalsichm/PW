using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_calendar_application.Abstractions;

namespace personal_calendar_presentation.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class ShareEventsController(IEventSharingService sharingService) : ControllerBase
{
    private readonly IEventSharingService _sharingService = sharingService;

    // [Authorize]
    [HttpGet("{id:guid}")]
    public IActionResult ShareEvents(Guid id)
    {
        var link = _sharingService.CreateLink(id);
        return Ok(link);
    }

    // [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ValidateURL(Guid user, string token, string timestamp, string expirationTime)
    {
        // return problem???
        if (!long.TryParse(timestamp, out var time) ||
            !long.TryParse(expirationTime, out var expiration))
        {
            return BadRequest(new { message = "Invalid time format." });
        }
        var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        if (time + expiration <= currentTime)
        {

            return BadRequest(new { message = "Link has expired" });
        }
        var parameters = user + timestamp + expirationTime;
        var events = await _sharingService.ReturnEventsIfValid(token, parameters, user);
        if (events is null) return NotFound(new { message = "This user doesn't exist or token is invalid" });
        return Ok(events);
    }
}