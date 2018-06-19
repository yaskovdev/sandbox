package preparation;

import org.junit.Assert;
import org.junit.Test;

public class LeftRotation {

    @Test
    public void test0() {
        Assert.assertArrayEquals(new int[]{2, 3, 4, 5, 1}, rotLeft(new int[]{1, 2, 3, 4, 5}, 1));
    }

    @Test
    public void test1() {
        Assert.assertArrayEquals(new int[]{5, 1, 2, 3, 4}, rotLeft(new int[]{1, 2, 3, 4, 5}, 4));
    }

    private static int[] rotLeft(int[] a, int d) {
        while (d > 0) {
            int temp = a[0];
            System.arraycopy(a, 1, a, 0, a.length - 1);
            a[a.length - 1] = temp;
            d--;
        }
        return a;
    }
}
