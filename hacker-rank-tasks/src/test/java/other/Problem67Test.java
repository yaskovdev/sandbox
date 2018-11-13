package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem67Test {

    @Test
    public void test1() {
        final String sum = new Problem67().addBinary("11", "1");
        assertEquals("100", sum);
    }

    @Test
    public void test2() {
        final String sum = new Problem67().addBinary("1010", "1011");
        assertEquals("10101", sum);
    }
}
