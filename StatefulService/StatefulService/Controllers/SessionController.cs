using Microsoft.AspNetCore.Mvc;

namespace StatefulService.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionService sessionService;
    private readonly ISessionEventHandler sessionEventHandler;

    public SessionController(ISessionService sessionService, ISessionEventHandler sessionEventHandler)
    {
        this.sessionService = sessionService;
        this.sessionEventHandler = sessionEventHandler;
    }

    [HttpGet("/{sessionId}/average-duration-of-events")]
    public double GetAverageDurationOfEvents(string sessionId) => sessionService.GetSession(sessionId).AverageDurationOfEvents;

    [HttpPost("/{sessionId}")]
    public void Create(string sessionId) => sessionService.CreateSession(sessionId);

    [HttpPost("/{sessionId}/events")]
    public void HandleEvent(string sessionId, [FromBody] SessionEvent sessionEvent) => sessionEventHandler.HandleEvent(sessionId, sessionEvent);
}
