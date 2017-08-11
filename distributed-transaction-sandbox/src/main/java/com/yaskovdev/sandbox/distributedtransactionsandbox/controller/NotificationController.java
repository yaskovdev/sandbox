package com.yaskovdev.sandbox.distributedtransactionsandbox.controller;

import com.yaskovdev.sandbox.distributedtransactionsandbox.client.JdbcClient;
import com.yaskovdev.sandbox.distributedtransactionsandbox.client.JmsClient;
import com.yaskovdev.sandbox.distributedtransactionsandbox.model.Notification;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import static org.springframework.web.bind.annotation.RequestMethod.POST;

@RestController
public class NotificationController {

    private final JdbcClient jdbcClient;

    private final JmsClient jmsClient;

    @Autowired
    public NotificationController(final JdbcClient jdbcClient, final JmsClient jmsClient) {
        this.jdbcClient = jdbcClient;
        this.jmsClient = jmsClient;
    }

    @RequestMapping(method = POST, path = "/notifications")
    public Notification createNotification() {
        jdbcClient.createEvent();
        jmsClient.sendNotification();
        return new Notification(123, "Content");
    }
}
