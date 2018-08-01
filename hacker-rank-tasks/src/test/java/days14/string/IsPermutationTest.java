package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class IsPermutationTest {

    @Test
    public void test1() {
        assertTrue(new IsPermutation().solve("abracadabra", "arbadacarba"));
    }

    @Test
    public void test2() {
        assertTrue(new IsPermutation().solve("a", "a"));
    }

    @Test
    public void test3() {
        assertFalse(new IsPermutation().solve("a", "aa"));
    }

    @Test
    public void test4() {
        assertFalse(new IsPermutation().solve("ab", "ac"));
    }
}
