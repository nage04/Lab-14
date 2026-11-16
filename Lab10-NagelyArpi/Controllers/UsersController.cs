using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Application.UseCases.Users.Commands;
using Lab10_NagelyArpi.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_NagelyArpi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class UsersController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<UserDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await sender.Send(new GetAllUsersQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await sender.Send(new GetUserByIdQuery(id), cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await sender.Send(command, cancellationToken) ? NoContent() : NotFound();
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
        catch (InvalidOperationException exception) { return Conflict(new { message = exception.Message }); }
    }
}
