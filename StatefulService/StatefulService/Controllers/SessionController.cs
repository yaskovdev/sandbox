using Microsoft.AspNetCore.Mvc;

namespace StatefulService.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionService sessionService;
    private readonly ILogger<SessionController> logger;

    public SessionController(ISessionService sessionService, ILogger<SessionController> logger)
    {
        this.sessionService = sessionService;
        this.logger = logger;
    }

    [HttpGet("/{sessionId}")]
    public Session Get(string sessionId) => sessionService.GetSession(sessionId);

    [HttpPost("/{sessionId}")]
    public Session Create(string sessionId)
    {
        var session = sessionService.CreateSession(sessionId);
        logger.LogInformation("Created session");
        return session;
    }

    [HttpPost("/{sessionId}/events")]
    public Session RegisterEvent(string sessionId, [FromBody] SessionEvent sessionEvent)
    {
        var session = sessionService.RegisterEvent(sessionId, sessionEvent);
        logger.LogInformation("Registered event");
        return session;
    }
}
