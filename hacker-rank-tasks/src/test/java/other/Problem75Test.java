package other;

import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;

public class Problem75Test {

    @Test
    public void test1() {
        final int[] numbers = new int[]{2, 0, 2, 1, 1, 0};
        new Problem75().sortColors(numbers);
        assertArrayEquals(new int[]{0, 0, 1, 1, 2, 2}, numbers);
    }
}
