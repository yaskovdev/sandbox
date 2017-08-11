package com.yaskovdev.sandbox.distributedtransactionsandbox.model;

public class Notification {

    private final long id;
    private final String content;

    public Notification(long id, String content) {
        this.id = id;
        this.content = content;
    }

    public long getId() {
        return id;
    }

    public String getContent() {
        return content;
    }
}