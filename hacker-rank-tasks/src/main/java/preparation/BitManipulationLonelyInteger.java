package preparation;

import org.junit.Assert;
import org.junit.Test;

public class BitManipulationLonelyInteger {

    @Test
    public void test0() {
        Assert.assertEquals(2, solution(new int[]{0, 0, 1, 2, 1}));
    }

    @Test
    public void test1() {
        Assert.assertEquals(5, solution(new int[]{5}));
    }

    @Test
    public void test2() {
        Assert.assertEquals(95, solution(new int[]{4, 9, 95, 93, 57, 4, 57, 93, 9}));
    }

    private static int solution(final int[] numbers) {
        int result = numbers[0];
        for (int i = 1; i < numbers.length; i++) {
            result ^= numbers[i];
        }
        return result;
    }
}
