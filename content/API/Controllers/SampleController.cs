using System.Net;
using Domain.Handlers.Sample.GetSample;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/v1/sample")]
public class SampleController : ControllerBase
{
    private readonly ILogger<SampleController> _logger;
    private readonly IMediator _mediator;

    public SampleController(ILogger<SampleController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    [ProducesResponseType(typeof(ResponseItem<CodeName>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Get([FromQuery] GetSample req)
    {
        _logger.LogInformation("SampleController");
        var res = await _mediator.Send(req);

        return Ok(res);
    }
}
