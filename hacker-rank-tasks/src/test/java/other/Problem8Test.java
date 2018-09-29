package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem8Test {

    @Test
    public void test1() {
        assertEquals(42, new Problem8().myAtoi("42"));
    }

    @Test
    public void test2() {
        assertEquals(-42, new Problem8().myAtoi("   -42"));
    }

    @Test
    public void test3() {
        assertEquals(4193, new Problem8().myAtoi("4193 with words"));
    }

    @Test
    public void test4() {
        assertEquals(0, new Problem8().myAtoi("words and 987"));
    }

    @Test
    public void test5() {
        assertEquals(Integer.MIN_VALUE, new Problem8().myAtoi("-91283472332"));
    }

    @Test
    public void test6() {
        assertEquals(Integer.MIN_VALUE, new Problem8().myAtoi("-6147483648"));
    }
}
