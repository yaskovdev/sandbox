using Microsoft.AspNetCore.Mvc;

namespace StatefulService.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionService sessionService;
    private readonly ISessionEventHandler sessionEventHandler;
    private readonly ILogger<SessionController> logger;

    public SessionController(ISessionService sessionService, ISessionEventHandler sessionEventHandler, ILogger<SessionController> logger)
    {
        this.sessionService = sessionService;
        this.sessionEventHandler = sessionEventHandler;
        this.logger = logger;
    }

    [HttpGet("/{sessionId}/average-duration-of-events")]
    public double GetAverageDurationOfEvents(string sessionId)
    {
        var session = sessionService.GetSession(sessionId);
        return session.AverageDurationOfEvents;
    }

    [HttpPost("/{sessionId}")]
    public void Create(string sessionId) => sessionService.CreateSession(sessionId);

    [HttpPost("/{sessionId}/events")]
    public void HandleEvent(string sessionId, [FromBody] SessionEvent sessionEvent)
    {
        sessionEventHandler.HandleEvent(sessionId, sessionEvent);
        logger.LogInformation("Registered event");
    }
}
