package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem3Test {

    @Test
    public void test1() {
        assertEquals(3, new Problem3().lengthOfLongestSubstring("abcabcbb"));
    }

    @Test
    public void test2() {
        assertEquals(1, new Problem3().lengthOfLongestSubstring("bbbbb"));
    }

    @Test
    public void test3() {
        assertEquals(3, new Problem3().lengthOfLongestSubstring("pwwkew"));
    }
}
