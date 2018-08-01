import com.rabbitmq.client.AMQP;
import com.rabbitmq.client.Channel;
import com.rabbitmq.client.DefaultConsumer;
import com.rabbitmq.client.Envelope;

import java.io.IOException;

class ActualConsumer extends DefaultConsumer {

    ActualConsumer(final Channel channel) {
        super(channel);
    }

    @Override
    public void handleDelivery(final String consumerTag, final Envelope envelope, final AMQP.BasicProperties properties, final byte[] body) throws IOException {
        System.out.println("Received " + new String(body) + " in thread " + Thread.currentThread().getName());
        final long deliveryTag = envelope.getDeliveryTag();
//        getChannel().basicAck(deliveryTag, false);
    }
}
