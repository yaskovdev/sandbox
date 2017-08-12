package com.yaskovdev.sandbox.jms;

import javax.jms.ExceptionListener;
import javax.jms.JMSException;

public class LoggingExceptionListener implements ExceptionListener {

    public synchronized void onException(JMSException exception) {
        System.out.println("JMS Exception occurred. Shutting down client.");
    }
}
