package org.jmm;

/**
 * If I run run1 and run2 concurrently, is it possible to see two 0 printed to the console?
 */
public class Example {

    private volatile int a = 0;
    private volatile int b = 0;

    public void run1() {
        b = 1; // 1
        int r2 = a; // 2
    }

    public void run2() {
        a = 2; // 3
        int r1 = b; // 4
    }
}

// Need to show a legal execution where both reads return 0. Here is one:
// int r2 = a;
// int r1 = b;
// b = 1;
// a = 2;

// 1 hb 2, 3 hb 4

// 4
// 2
// 3
// 1
