package other;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class Problem680Test {

    @Test
    public void test1() {
        assertTrue(new Problem680().validPalindrome("aba"));
    }

    @Test
    public void test2() {
        assertTrue(new Problem680().validPalindrome("abca"));
    }

    @Test
    public void test3() {
        assertTrue(new Problem680().validPalindrome("ab"));
    }

    @Test
    public void test4() {
        assertTrue(new Problem680().validPalindrome("abccbda"));
    }

    @Test
    public void test5() {
        assertFalse(new Problem680().validPalindrome("abc"));
    }
}
