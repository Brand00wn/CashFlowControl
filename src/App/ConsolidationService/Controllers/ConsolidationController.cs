using Application.Consolidation.Consolidation.Query.GetAllConsolidations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsolidationService.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ConsolidationController(IMediator _mediator, ILogger<ConsolidationController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _mediator.Send(new GetAllConsolidationsQuery());

            _logger.LogInformation("Retrieved all consolidations.");
            return Ok(result);
        }
        catch (Exception)
        {
            _logger.LogError("Failed to retrieve consolidations.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
