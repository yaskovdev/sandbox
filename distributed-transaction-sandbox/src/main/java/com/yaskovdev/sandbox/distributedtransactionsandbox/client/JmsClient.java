package com.yaskovdev.sandbox.distributedtransactionsandbox.client;

import com.yaskovdev.sandbox.distributedtransactionsandbox.exception.CannotSendNotificationException;
import com.yaskovdev.sandbox.distributedtransactionsandbox.model.Notification;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.springframework.stereotype.Component;

import javax.jms.Connection;
import javax.jms.DeliveryMode;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;

import static java.lang.Thread.currentThread;

@Component
public class JmsClient {

    public void sendNotification(Notification notification) {
        try {
            ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory("tcp://127.0.0.1:61616");

            Connection connection = connectionFactory.createConnection();
            connection.start();

            Session session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

            Destination destination = session.createQueue("TEST.FOO");

            MessageProducer producer = session.createProducer(destination);
            producer.setDeliveryMode(DeliveryMode.NON_PERSISTENT);

            String text = "Got " + notification + " from: " + currentThread().getName() + " : " + hashCode();
            TextMessage message = session.createTextMessage(text);

            System.out.println("Sent message: " + message.hashCode() + " : " + currentThread().getName());
            producer.send(message);

            session.close();
            connection.close();
        } catch (final JMSException e) {
            throw new CannotSendNotificationException(e);
        }
    }
}
