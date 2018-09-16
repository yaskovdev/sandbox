package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem91Test {

    @Test
    public void test1() {
        final int num = new Problem91().numDecodings("12");
        assertEquals(2, num);
    }

    @Test
    public void test2() {
        final int num = new Problem91().numDecodings("226");
        assertEquals(3, num);
    }

    @Test
    public void test3() {
        final int num = new Problem91().numDecodings("2031");
        assertEquals(1, num);
    }

    @Test
    public void test4() {
        final int num = new Problem91().numDecodings("2131");
        assertEquals(3, num);
    }
}
