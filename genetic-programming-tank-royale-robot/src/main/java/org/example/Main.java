package org.example;

public class Main {
    public static void main(String[] args) throws InterruptedException {
        Thread thread = new Thread(() -> {
            System.out.println("Starting instance 1");
            new MyFirstBot().start();
        });
        thread.start();
        System.out.println("Starting instance 2");
        new MyFirstBot().start();
        thread.join();
    }
}