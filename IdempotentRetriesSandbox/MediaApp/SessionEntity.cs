namespace MediaApp;

public record SessionEntity(string CallId, SessionState State, DateTime CreatedAt, DateTime LeaseExpiresAt);
