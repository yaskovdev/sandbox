using Microsoft.AspNetCore.Mvc;

namespace StatefulServiceProblem.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionEventHandlers sessionEventHandlers;

    public SessionController(ISessionEventHandlers sessionEventHandlers) => this.sessionEventHandlers = sessionEventHandlers;

    [HttpGet("/{sessionId}/average-duration-of-events")]
    public double GetAverageDurationOfEvents(string sessionId) => sessionEventHandlers.Get(sessionId).AverageDurationOfEvents;

    [HttpPost("/{sessionId}")]
    public void Create(string sessionId) => sessionEventHandlers.Add(sessionId, new StatefulSessionEventHandler());

    [HttpPost("/{sessionId}/events")]
    public void HandleEvent(string sessionId, [FromBody] SessionEvent sessionEvent) => sessionEventHandlers.Get(sessionId).HandleEvent(sessionEvent);
}
