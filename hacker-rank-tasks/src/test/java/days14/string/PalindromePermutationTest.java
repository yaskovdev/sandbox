package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class PalindromePermutationTest {

    @Test
    public void test1() {
        assertTrue(new PalindromePermutation().solve("нажал кабан на баклажан"));
    }

    @Test
    public void test2() {
        assertTrue(new PalindromePermutation().solve("бел хлеб"));
    }

    @Test
    public void test3() {
        assertTrue(new PalindromePermutation().solve("Tact Coa"));
    }

    @Test
    public void test4() {
        assertTrue(new PalindromePermutation().solve("Лазер Боре хер обрезал"));
    }

    @Test
    public void test5() {
        assertFalse(new PalindromePermutation().solve("был хлеб"));
    }
}
