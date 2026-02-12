namespace AzureServiceBusTransactions;

using System.Transactions;
using Azure.Messaging.ServiceBus;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var queueName = "sandbox_queue";
        await using ServiceBusClient client = new("");
        var sender = client.CreateSender(queueName);

        var fail = true;
        await sender.SendMessageAsync(new ServiceBusMessage("First"u8.ToArray()));
        var receiver = client.CreateReceiver(queueName);
        var firstMessage = await receiver.ReceiveMessageAsync();
        using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await sender.SendMessageAsync(new ServiceBusMessage("Second"u8.ToArray()));
            if (fail)
            {
                throw new Exception("Simulated failure");
            }
            await receiver.CompleteMessageAsync(firstMessage);
            ts.Complete();
        }
    }
}
