## Prerequisites

Start Redis in Docker:

```shell
docker run --name redis_instance -d -p 6379:6379 redis
```

Start the LoadBalancer.

Start all the MediaApp instances (see the launchSettings.json).

Start the Watchdog.

## Testing

```shell
./submit_calls.sh
```

## Assumptions

1. Once Redis operation is successful, its visible to all MediaApp instances. This can be guaranteed by meeting both of
   the following conditions:
    1. All the MediaApp instances share the same Redis cluster.
    2. A change to one Redis node is synchronously replicated to all other nodes in the Redis cluster.
2. Lua scripts are atomic (TODO: check consequences of that they are not true ACID transactions).
3. All the requests that may lead to a new session creation are routed through the LoadBalancer. If a session was
   actually created, the MediaApp instance returns a 201 Created response.

## Guarantees

1. Each session will be eventually assigned a worker, as long as the workers have available slots.
2. More than one worker may be assigned to a session. For example:
    - Worker A is assigned to session S1.
    - Worker A gets partitioned from Redis, which prevents it from extending the lease of the session.
    - Watchdog detects that S1 is not owned by any worker and assigns it to Worker B.
3. If the `/calls/{callId}` responds with a 2**, then the call will be eventually assigned a worker. A call is assigned
   a worker if one or more of its sessions is assigned a worker.
4. If the `/calls/{callId}` responds with a code other than 2**, then the call may or may not be assigned a worker.

## Considerations

For each session, an instance can periodically check if there is another instance that is handling a session of the same
call.

If it is, the instance can *inactivate the session* and stop session processing.

If the instance can't check, for example, because it is partitioned from Redis, it has two options:

1. Keep processing the session and extending the lease. In this case, there is a chance that a call will multiple
   parallel sessions.
2. Inactivate the session and stop processing it. In this case, there is a chance that the call will not be processed at
   all.

There is a risk though that the other instance will do the same. As a result, both the sessions will be stopped, which
is not desired. We probably need a way to order instances.
