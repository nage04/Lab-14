using Lab10_NagelyArpi.Application.UseCases.Roles.Commands;
using Lab10_NagelyArpi.Application.UseCases.Roles.Queries;
using Lab10_NagelyArpi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_NagelyArpi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class RolesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { roleId = id }, id);
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Role>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Role>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await sender.Send(new GetAllRolesQuery(), cancellationToken));

    [HttpGet("{roleId:guid}")]
    [ProducesResponseType<Role>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Role>> GetById(Guid roleId, CancellationToken cancellationToken)
    {
        var role = await sender.Send(new GetRoleByIdQuery(roleId), cancellationToken);
        return role is null ? NotFound() : Ok(role);
    }
}
