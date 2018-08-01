package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class OneAwayTest {

    @Test
    public void test1() {
        assertTrue(new OneAway().solve("pale", "ple"));
    }

    @Test
    public void test2() {
        assertTrue(new OneAway().solve("pales", "pale"));
    }

    @Test
    public void test3() {
        assertTrue(new OneAway().solve("pale", "bale"));
    }

    @Test
    public void test4() {
        assertFalse(new OneAway().solve("pale", "bake"));
    }
}