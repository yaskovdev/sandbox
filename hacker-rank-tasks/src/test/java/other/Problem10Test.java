package other;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class Problem10Test {

    @Test
    public void test8() {
        assertTrue(new Problem10().isMatch("mississippi", "mississippi"));
    }

    @Test
    public void test9() {
        assertFalse(new Problem10().isMatch("mississippi", "messesseppe"));
    }

    @Test
    public void test10() {
        assertFalse(new Problem10().isMatch("missis", "mississ"));
    }

    @Test
    public void test11() {
        assertFalse(new Problem10().isMatch("mississ", "missis"));
    }

    @Test
    public void test12() {
        assertTrue(new Problem10().isMatch("mississippi", "m.ssissi..i"));
    }

    @Test
    public void test1() {
        assertFalse(new Problem10().isMatch("mississippi", "mis*is*p*."));
    }

    @Test
    public void test2() {
        assertTrue(new Problem10().isMatch("mississippi", "m.s*is*ip*i"));
    }

    @Test
    public void test4() {
        assertFalse(new Problem10().isMatch("aa", "a"));
    }

    @Test
    public void test5() {
        assertTrue(new Problem10().isMatch("aa", "a*"));
    }

    @Test
    public void test6() {
        assertTrue(new Problem10().isMatch("ab", ".*"));
    }

    @Test
    public void test3() {
        assertTrue(new Problem10().isMatch("aab", "c*a*b"));
    }

    @Test
    public void test7() {
        assertTrue(new Problem10().isMatch("aaa", "a*a"));
    }

    @Test
    public void test13() {
        assertTrue(new Problem10().isMatch("aaab", "a*b"));
    }

    @Test
    public void test14() {
        assertFalse(new Problem10().isMatch("", "c*ab"));
    }

    @Test
    public void test15() {
        assertFalse(new Problem10().isMatch("acaabbaccbbacaabbbb", "a*.*b*.*a*aa*a*"));
    }
}
