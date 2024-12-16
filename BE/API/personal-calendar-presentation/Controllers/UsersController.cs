using System.Net.Mail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_calendar_application.Events.Queries;
using personal_calendar_application.Users.Commands.Create;
using personal_calendar_application.Users.Commands.Delete;
using personal_calendar_application.Users.Contracts;
using personal_calendar_application.Users.Queries.Get;
using personal_calendar_application.Users.Queries.List;
using personal_calendar_presentation.Contracts;

namespace personal_calendar_presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    // private readonly IUserService _userService;
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _sender.Send(new GetUsersQuery());
        return Ok(users);
    }


    // Cancellation Tokens??


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _sender.Send(new GetUserQuery(id));
        return user is not null ? Ok(user) : NotFound();
    }

    [HttpGet("{id:guid}/events")]
    public async Task<IActionResult> GetUserEvents(Guid id)
    {
        var events = await _sender.Send(new GetEventsQuery(id));
        if (events is null) return NotFound(new { message = "User not found" });
        return Ok(events);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserOrAdminRequest request)
    {
        try
        {
            var mail = new MailAddress(request.Email);
        }
        catch
        {
            return BadRequest(new { message = "Invalid email address." });
        }
        var command = CreateUserCommand.CreateCommand(request);
        var user = await _sender.Send(command);
        if (user is null) return BadRequest(new { message = "This email is taken." });
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = UpdateUserCommand.CreateCommand(request);
        var updated_user = await _sender.Send(command);
        return updated_user is null ? NotFound() : Ok(updated_user);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _sender.Send(new DeleteUserCommand(id));
        return NoContent();
    }



}