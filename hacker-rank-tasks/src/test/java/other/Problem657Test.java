package other;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class Problem657Test {

    @Test
    public void test0() {
        assertTrue(new Problem657().judgeCircle(""));
    }

    @Test
    public void test1() {
        assertTrue(new Problem657().judgeCircle("UULDDR"));
    }

    @Test
    public void test2() {
        assertFalse(new Problem657().judgeCircle("UULDR"));
    }
}