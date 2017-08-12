package com.yaskovdev.sandbox.distributedtransactionsandbox.model;

public class Notification {

    private String type;
    private String name;

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
