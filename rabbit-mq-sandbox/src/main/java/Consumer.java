import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

public class Consumer {

    public static void main(String[] args) throws IOException, TimeoutException {
        final String queue = "myFirstQueue";
        final ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        try (final Connection connection = factory.newConnection(); final Channel channel = connection.createChannel()) {
            channel.queueDeclare("myFirstQueue", true, false, false, null);
            // tag and tag2 will be executed by the same thread
            final String tag = channel.basicConsume(queue, false, "1", new ActualConsumer(channel));
            final String tag2 = channel.basicConsume(queue,false, "2", new ActualConsumer(channel));
            System.in.read();
            channel.basicCancel(tag);
        }
    }
}
