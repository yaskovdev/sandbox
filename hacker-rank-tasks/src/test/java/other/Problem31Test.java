package other;

import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;

public class Problem31Test {

    @Test
    public void test1() {
        final int[] numbers = {1, 2, 3};
        new Problem31().nextPermutation(numbers);
        assertArrayEquals(new int[]{1, 3, 2}, numbers);
    }

    @Test
    public void test2() {
        final int[] numbers = {3, 2, 1};
        new Problem31().nextPermutation(numbers);
        assertArrayEquals(new int[]{1, 2, 3}, numbers);
    }

    @Test
    public void test3() {
        final int[] numbers = {1, 1, 5};
        new Problem31().nextPermutation(numbers);
        assertArrayEquals(new int[]{1, 5, 1}, numbers);
    }
}
