package days14.stack;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class StackMinTest {

    @Test
    public void min() {
        final StackMin stack = new StackMin();
        stack.push(3);
        stack.push(5);
        stack.push(-100);
        stack.push(9);
        stack.pop();
        stack.pop();
        assertEquals(3, stack.min());
    }
}