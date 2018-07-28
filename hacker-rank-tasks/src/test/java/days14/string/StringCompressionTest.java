package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class StringCompressionTest {

    @Test
    public void test1() {
        assertEquals("a2b1c5a3", new StringCompression().compress("aabcccccaaa"));
    }

    @Test
    public void test2() {
        assertEquals("a", new StringCompression().compress("a"));
    }

    @Test
    public void test3() {
        assertEquals("", new StringCompression().compress(""));
    }
}
