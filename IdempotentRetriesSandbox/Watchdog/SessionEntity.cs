namespace Watchdog;

public record SessionEntity(SessionState State, DateTime CreatedAt, DateTime UpdatedAt);
