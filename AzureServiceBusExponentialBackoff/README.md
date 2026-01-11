# Azure Service Bus Exponential Backoff

Inspired by https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-deferral and,
specifically, https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Azure.Messaging.ServiceBus/samples/Sample02_MessageSettlement.md#defer-a-message.

## Test Scenarios

Check that the lock duration of the queue is 30 seconds.

### 1 and 2

Make sure the queue is empty. You can run 0 to empty the queue.

Run 1. Then after 2 seconds but before 30 seconds, run 2.

As a result, in 2, the deferred message will be received before the original message (because the original message will
still be locked).

### 3 and 4

Make sure the queue is empty. You can run 0 to empty the queue.

Run 3. Then before 30 seconds, run 4. Wait for the lock to expire (30 seconds).

As a result, in 4, the original message will be received before the deferred message.

##    

Q: Can I defer a deferred message? Will the properties be updated?
A: Yes, it's possible. But you must receive the message before deferring. You can't receive then defer multiple times.

Q: Can I complete a deferred message?
A: Yes, but only once, because completion deletes the message from the queue. If you call the
`ReceiveDeferredMessageAsync` for the same `SequenceNumber`, you'll get "Azure.Messaging.ServiceBus.ServiceBusException:
Failed to lock one or more specified messages. Either the message is already locked or does not exist."

Q: Does `ServiceBusReceivedMessage.DeliveryCount` increment if I receive and then defer the message?
A: Yes. So there's no need to pass the modified properties to `DeferMessageAsync`.

Q: What if I first send a ref message, then fail to defer it; then I receive the original message again, fail to handle
it and send a new ref message? There will be two ref messages pointing to the same original message.

Q: Is the following possible?
1. Send a message.
2. Receive it and send a reference to it.
3. Receive it again, send another reference, then defer it.
4. Receive both the references from different instances and start processing the message sent in 1 twice in parallel.