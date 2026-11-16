using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Application.UseCases.Tickets.Commands;
using Lab10_NagelyArpi.Application.UseCases.Tickets.Queries;
using Lab10_NagelyArpi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_NagelyArpi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class TicketsController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateTicketCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { ticketId = id }, id);
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
        catch (KeyNotFoundException exception) { return NotFound(new { message = exception.Message }); }
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Ticket>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await sender.Send(new GetAllTicketsQuery(), cancellationToken));

    [HttpGet("{ticketId:guid}")]
    [ProducesResponseType<Ticket>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Ticket>> GetById(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await sender.Send(new GetTicketByIdQuery(ticketId), cancellationToken);
        return ticket is null ? NotFound() : Ok(ticket);
    }

    [HttpGet("by-user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetByUser(Guid userId, CancellationToken cancellationToken) =>
        Ok(await sender.Send(new GetTicketsByUserQuery(userId), cancellationToken));

    [HttpPatch("{ticketId:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid ticketId, [FromBody] string status, CancellationToken cancellationToken)
    {
        try
        {
            return await sender.Send(new UpdateTicketStatusCommand(ticketId, status), cancellationToken)
                ? NoContent() : NotFound();
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
    }

    [HttpDelete("{ticketId:guid}")]
    public async Task<IActionResult> Delete(Guid ticketId, CancellationToken cancellationToken) =>
        await sender.Send(new DeleteTicketCommand(ticketId), cancellationToken) ? NoContent() : NotFound();
}
