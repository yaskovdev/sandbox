package other;

import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;

public class Problem349Test {

    @Test
    public void test1() {
        assertArrayEquals(new int[]{2}, new Problem349().intersection(new int[]{1, 2, 2, 1}, new int[]{2, 2}));
    }

    @Test
    public void test2() {
        assertArrayEquals(new int[]{4, 9}, new Problem349().intersection(new int[]{4, 9, 5}, new int[]{9, 4, 9, 8, 4}));
    }
}
