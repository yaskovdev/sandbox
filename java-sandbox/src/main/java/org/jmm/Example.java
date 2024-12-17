package org.jmm;

/**
 * If I run method1 and method2 concurrently, is it possible to see two 0 printed to the console?
 */
public class Example {

    private volatile int a = 0;
    private volatile int b = 0;

    public void run1() {
        b = 1;
        int r2 = a;
        System.out.println(r2);
    }

    public void run2() {
        a = 2;
        int r1 = b;
        System.out.println(r1);
    }
}

// Need to show a legal execution where both reads return 0. Here is one:
// int r2 = a;
// int r1 = b;
// b = 1;
// a = 2;