package interview;

import org.junit.Test;

import static java.util.Arrays.copyOf;
import static java.util.Arrays.sort;
import static org.junit.Assert.assertArrayEquals;

public class Second {

    @Test
    public void test1() {
        assertArrayEquals(new int[]{1, 2, 3}, testedObject().solution(new int[]{3, 1, 2}));
    }

    private static Second testedObject() {
        return new Second();
    }

    public int[] solution(final int[] array) {
        final int[] copy = copyOf(array, array.length);
        sort(copy);
        return copy;
    }
}
