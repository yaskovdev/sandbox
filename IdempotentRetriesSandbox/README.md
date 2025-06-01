## Prerequisites

```shell
docker run --name redis_instance -d -p 6379:6379 redis
```

## Testing

```shell
curl -X POST "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174000" -H "Content-Type: application/json"
curl -X POST "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174001" -H "Content-Type: application/json"

curl -X DELETE "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174000" -H "Content-Type: application/json"
curl -X DELETE "http://localhost:5032/sessions/123e4567-e89b-12d3-a456-426614174001" -H "Content-Type: application/json"
```

## Assumptions

1. Once Redis operation is successful, its visible to all MediaApp instances. This can be guaranteed by meeting both of
   the following conditions:
    1. All the MediaApp instances share the same Redis cluster.
    2. A change to one Redis node is synchronously replicated to all other nodes in the Redis cluster.
2. Lua scripts are atomic (TODO: check consequences of that they are not true ACID transactions).

## Guarantees

1. Each session will be eventually assigned a worker.
2. More than one worker may be assigned to a session. For example:
   - Worker A is assigned to session S1.
   - Worker A gets partitioned from Redis, which prevents it from extending the lease of the session.
   - Watchdog detects that S1 is not owned by any worker and assigns it to Worker B.
