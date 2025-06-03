namespace Watchdog;

public record Session(string Id, string CallId, DateTime CreatedAt, DateTime LeaseExpiresAt);
