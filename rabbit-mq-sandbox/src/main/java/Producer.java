import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.MessageProperties;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

public class Producer {

    public static void main(String[] args) throws IOException, TimeoutException {
        final ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        try (final Connection connection = factory.newConnection(); final Channel channel = connection.createChannel()) {
            channel.queueDeclare("myFirstQueue", true, false, false, null);
//            channel.basicPublish("", "myFirstQueue", null, "My First Message".getBytes());
            channel.basicPublish("", "myFirstQueue", MessageProperties.PERSISTENT_TEXT_PLAIN, "My First Persistent Message".getBytes());
        }
    }
}
