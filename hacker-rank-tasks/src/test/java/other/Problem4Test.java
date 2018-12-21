package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem4Test {

    @Test
    public void test1() {
        assertEquals(2.0, new Problem4().findMedianSortedArrays(new int[]{1, 3}, new int[]{2}), 0);
    }

    @Test
    public void test2() {
        assertEquals(2.5, new Problem4().findMedianSortedArrays(new int[]{1, 2}, new int[]{3, 4}), 0);
    }
}
