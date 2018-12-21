package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem76Test {

    @Test
    public void test1() {
        assertEquals("BANC", new Problem76().minWindow("ADOBECODEBANC", "ABC"));
    }

    @Test
    public void test2() {
        assertEquals("", new Problem76().minWindow("a", "aa"));
    }

    @Test
    public void test3() {
        assertEquals("", new Problem76().minWindow("ab", "A"));
    }

    @Test
    public void test4() {
        assertEquals("ba", new Problem76().minWindow("acbab", "ab"));
    }

}