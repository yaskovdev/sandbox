using Microsoft.AspNetCore.Mvc;

namespace StatefulServiceProblem.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionEventHandlers sessionEventHandlers;
    private readonly ILogger<SessionController> logger;

    public SessionController(ISessionEventHandlers sessionEventHandlers, ILogger<SessionController> logger)
    {
        this.sessionEventHandlers = sessionEventHandlers;
        this.logger = logger;
    }

    [HttpGet("/{sessionId}/average-duration-of-events")]
    public double GetAverageDurationOfEvents(string sessionId)
    {
        var eventHandler = sessionEventHandlers.Get(sessionId);
        return eventHandler.AverageDurationOfEvents;
    }

    [HttpPost("/{sessionId}")]
    public void Create(string sessionId) => sessionEventHandlers.Add(sessionId, new StatefulSessionEventHandler());

    [HttpPost("/{sessionId}/events")]
    public void HandleEvent(string sessionId, [FromBody] SessionEvent sessionEvent)
    {
        var eventHandler = sessionEventHandlers.Get(sessionId);
        eventHandler.HandleEvent(sessionEvent);
        logger.LogInformation("Registered event");
    }
}
