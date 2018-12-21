package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem7Test {

    @Test
    public void test0() {
        assertEquals(21, new Problem7().reverse(120));
    }

    @Test
    public void test1() {
        assertEquals(-321, new Problem7().reverse(-123));
    }

    @Test
    public void test2() {
        assertEquals(0, new Problem7().reverse(0));
    }
}
