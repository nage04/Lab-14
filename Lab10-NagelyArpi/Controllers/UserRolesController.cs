using Lab10_NagelyArpi.Application.UseCases.UserRoles.Commands;
using Lab10_NagelyArpi.Application.UseCases.UserRoles.Queries;
using Lab10_NagelyArpi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_NagelyArpi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class UserRolesController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Assign(AssignRoleToUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var assigned = await sender.Send(command, cancellationToken);
            return assigned ? CreatedAtAction(nameof(GetByUser), new { userId = command.UserId }, command) : Conflict();
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
        catch (KeyNotFoundException exception) { return NotFound(new { message = exception.Message }); }
    }

    [HttpGet("by-user/{userId:guid}")]
    [ProducesResponseType<IEnumerable<UserRole>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserRole>>> GetByUser(Guid userId, CancellationToken cancellationToken) =>
        Ok(await sender.Send(new GetUserRolesByUserIdQuery(userId), cancellationToken));

    [HttpDelete("{userId:guid}/{roleId:guid}")]
    public async Task<IActionResult> Remove(Guid userId, Guid roleId, CancellationToken cancellationToken) =>
        await sender.Send(new RemoveRoleFromUserCommand(userId, roleId), cancellationToken) ? NoContent() : NotFound();
}
