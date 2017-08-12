package com.yaskovdev.sandbox.distributedtransactionsandbox.model;

public class Notification {

    private String type;
    private String name;

    private Notification() {
    }

    public Notification(final String type, final String name) {
        this.type = type;
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public String getName() {
        return name;
    }

    @Override
    public String toString() {
        return "Notification{" +
                "type='" + type + '\'' +
                ", name='" + name + '\'' +
                '}';
    }
}
