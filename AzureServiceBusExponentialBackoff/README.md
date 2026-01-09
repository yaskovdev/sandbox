# Azure Service Bus Exponential Backoff

Inspired by https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-deferral and,
specifically, https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Azure.Messaging.ServiceBus/samples/Sample02_MessageSettlement.md#defer-a-message.

##  

Q: Can I defer a deferred message? Will the properties be updated?
A: Yes, it's possible. But you must receive the message before deferring. You can't receive then defer multiple times.

Q: Can I complete a deferred message?
A: Yes, but only once, because completion deletes the message from the queue. If you call the
`ReceiveDeferredMessageAsync` for the same `SequenceNumber`, you'll get "Azure.Messaging.ServiceBus.ServiceBusException:
Failed to lock one or more specified messages. Either the message is already locked or does not exist."

Q: Does `ServiceBusReceivedMessage.DeliveryCount` increment if I receive and then defer the message?
A: Yes. So there's no need to pass the modified properties to `DeferMessageAsync`.
