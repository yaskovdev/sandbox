package other;

import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;

public class Problem238Test {

    @Test
    public void test() {
        assertArrayEquals(new int[]{24, 12, 8, 6}, new Problem238().productExceptSelf(new int[]{1, 2, 3, 4}));
    }
}
