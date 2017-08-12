package com.yaskovdev.sandbox.distributedtransaction.client;

import com.yaskovdev.sandbox.distributedtransaction.exception.CannotSendNotificationException;
import com.yaskovdev.sandbox.distributedtransaction.model.Notification;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.slf4j.Logger;
import org.springframework.stereotype.Component;

import javax.jms.Connection;
import javax.jms.DeliveryMode;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;

import static java.lang.Thread.currentThread;
import static org.slf4j.LoggerFactory.getLogger;

@Component
public class JmsClient {

    private static final Logger logger = getLogger(JmsClient.class);

    public void sendNotification(final Notification notification) {
        try {
            final ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://127.0.0.1:61616");

            final Connection connection = connectionFactory.createConnection();
            connection.start();

            final Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

            final Destination destination = session.createQueue("TEST.FOO");

            final MessageProducer producer = session.createProducer(destination);
            producer.setDeliveryMode(DeliveryMode.NON_PERSISTENT);

            final String text = "Got " + notification + " from: " + currentThread().getName() + " : " + hashCode();
            final TextMessage message = session.createTextMessage(text);

            logger.info("Sent message: " + message.hashCode() + " : " + currentThread().getName());
            producer.send(message);

            session.close();
            connection.close();
        } catch (final JMSException e) {
            throw new CannotSendNotificationException(e);
        }
    }
}
