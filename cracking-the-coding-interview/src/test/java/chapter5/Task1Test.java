package chapter5;

import org.junit.Test;

import static java.lang.Integer.toBinaryString;
import static org.junit.Assert.assertEquals;

public class Task1Test {

    @Test
    public void test1() {
        assertEquals("10001001100", toBinaryString(new Task1().solve(1024, 19, 2, 6)));
    }

    @Test
    public void test2() {
        assertEquals("1101", toBinaryString(new Task1().solve(15, 0, 1, 1)));
    }

    @Test
    public void test3() {
        assertEquals("1001", toBinaryString(new Task1().solve(15, 0, 1, 2)));
    }
}
