package com.yaskovdev.sandbox.distributedtransactionsandbox.exception;

public class CannotSendNotificationException extends RuntimeException {

    public CannotSendNotificationException(final Throwable throwable) {
        super(throwable);
    }
}
