package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem29Test {

    @Test
    public void test() {
        assertEquals(3, new Problem29().divide(10, 3));
    }

    @Test
    public void test2() {
        assertEquals(-2, new Problem29().divide(7, -3));
    }

    @Test
    public void test3() {
        assertEquals(0, new Problem29().divide(-2147483648, -1));
    }
}
