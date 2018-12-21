package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem560Test {

    @Test
    public void test1() {
        assertEquals(2, new Problem560().subarraySum(new int[]{1, 1, 1}, 2));
    }

    @Test
    public void test2() {
        assertEquals(2, new Problem560().subarraySum(new int[]{2, 1, 3, 0}, 4));
    }
}
