package days14.stack;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * 3.1 (page 98)
 */
public class Stack {

    @Test
    public void test() {
        final Stack stack = new Stack();
        stack.push1(1);
        stack.push1(2);
        stack.push1(3);
        stack.push2(1);
        stack.push2(2);
        stack.push2(3);
        stack.push3(10);
        assertEquals(10, stack.pop3());
        stack.pop1();
        assertEquals(2, stack.pop1());
        assertEquals(3, stack.pop2());
    }

    private int[] a = new int[100];
    private int i = 0, j = 1, k = 2;

    void push1(final int value) {
        a[i] = value;
        i += 3;
    }

    int pop1() {
        i -= 3;
        return a[i];
    }

    void push2(final int value) {
        a[j] = value;
        j += 3;
    }

    int pop2() {
        j -= 3;
        return a[j];
    }

    void push3(final int value) {
        a[k] = value;
        k += 3;
    }

    int pop3() {
        k -= 3;
        return a[k];
    }
}
