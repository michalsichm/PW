using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Users.Queries.Get;

namespace personal_calendar_presentation.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class ShareEventsController(IEventSharingService sharingService, ISender sender) : ControllerBase
{
    private readonly IEventSharingService _sharingService = sharingService;
    private readonly ISender _sender = sender;


    [Authorize(Roles = "User")]
    [HttpGet("{id:guid}")]
    public IActionResult ShareEventsAsync(Guid id)
    {
        var link = _sharingService.CreateLink(id);
        if (string.IsNullOrEmpty(link))
        {
            return BadRequest(new { Error = "Unable to generate link." });
        }
        return Ok(new { link });

    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ValidateURL(Guid user, string token, string timestamp, string expirationTime)
    {
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
        var foundUser = await _sender.Send(new GetUserQuery(user));

        return Ok(new { name = foundUser.Name, events });

    }
}