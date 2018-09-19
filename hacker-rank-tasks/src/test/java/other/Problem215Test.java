package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Problem215Test {

    @Test
    public void test1() {
        assertEquals(5, new Problem215().findKthLargest(new int[]{3, 2, 1, 5, 6, 4}, 2));
    }

    @Test
    public void test2() {
        assertEquals(4, new Problem215().findKthLargest(new int[]{3, 2, 3, 1, 2, 4, 5, 5, 6}, 4));
    }
}