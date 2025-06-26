Make sure that the `AZURE_SERVICE_BUS_CONNECTION_STRING` environment variable is set to the connection string of your
Azure Service Bus instance.

## Observations

1. A dead-letter queue should be explicitly enable for a queue.
2. Apparently, the message-level TTL cannot be bigger than the queue-level TTL (see `ServiceBusMessage.TimeToLive`).
3. A max message TTL is unbounded.
4. An expired message may still be visible in the queue until someone tries to peek or receive it. Then it won't receive
   it and the message will go to the dead-letter queue.
5. Should be possible to purge the dead-letter queue, but it does not work for
   me: https://learn.microsoft.com/en-us/azure/service-bus-messaging/batch-delete#using-azure-portal.
6. If the `MaxAutoLockRenewalDuration` is infinite, but the instance crash-stops, the message will be returned to the
   queue with incremented delivery count. Most likely need to configure the max delivery count properly (can vary from 1
   to 2000).