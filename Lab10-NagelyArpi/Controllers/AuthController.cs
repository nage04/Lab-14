using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Application.UseCases.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_NagelyArpi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType<UserDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UserDto>> Register(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await sender.Send(command, cancellationToken);
            return Created($"/api/users/{user.Id}", user);
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
        catch (InvalidOperationException exception) { return Conflict(new { message = exception.Message }); }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType<TokenDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenDto>> Login(LoginCommand command, CancellationToken cancellationToken)
    {
        var token = await sender.Send(command, cancellationToken);
        return token is null ? Unauthorized() : Ok(token);
    }
}

// test de despliegue