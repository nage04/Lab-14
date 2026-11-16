using Lab10_NagelyArpi.Application.UseCases.Responses.Commands;
using Lab10_NagelyArpi.Application.UseCases.Responses.Queries;
using Lab10_NagelyArpi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_NagelyArpi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class ResponsesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(CreateResponseCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var id = await sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetByTicket), new { ticketId = command.TicketId }, id);
        }
        catch (ArgumentException exception) { return BadRequest(new { message = exception.Message }); }
        catch (KeyNotFoundException exception) { return NotFound(new { message = exception.Message }); }
    }

    [HttpGet("by-ticket/{ticketId:guid}")]
    [ProducesResponseType<IEnumerable<Response>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Response>>> GetByTicket(Guid ticketId, CancellationToken cancellationToken) =>
        Ok(await sender.Send(new GetResponsesByTicketIdQuery(ticketId), cancellationToken));
}
