package other;

import org.junit.Test;

import static java.util.Arrays.copyOfRange;
import static org.junit.Assert.assertArrayEquals;

public class Problem27Test {

    @Test
    public void test0() {
        final int[] array = {1};
        final int length = new Problem27().removeElement(array, 1);
        assertArrayEquals(new int[]{}, copyOfRange(array, 0, length));
    }

    @Test
    public void test1() {
        final int[] array = {3, 1, 5, 2};
        final int length = new Problem27().removeElement(array, 1);
        assertArrayEquals(new int[]{3, 5, 2}, copyOfRange(array, 0, length));
    }

    @Test
    public void test2() {
        final int[] array = {1, 1, 1};
        final int length = new Problem27().removeElement(array, 1);
        assertArrayEquals(new int[]{}, copyOfRange(array, 0, length));
    }

    @Test
    public void test3() {
        final int[] array = {};
        final int length = new Problem27().removeElement(array, 1);
        assertArrayEquals(new int[]{}, copyOfRange(array, 0, length));
    }

    @Test
    public void test4() {
        final int[] array = {21, 22, 23};
        final int length = new Problem27().removeElement(array, 24);
        assertArrayEquals(new int[]{21, 22, 23}, copyOfRange(array, 0, length));
    }

    @Test
    public void test5() {
        final int[] array = {3, 2, 2, 3};
        final int length = new Problem27().removeElement(array, 3);
        assertArrayEquals(new int[]{2, 2}, copyOfRange(array, 0, length));
    }
}
