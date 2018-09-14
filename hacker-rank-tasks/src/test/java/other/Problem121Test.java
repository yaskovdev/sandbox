package other;

import org.junit.Test;

public class Problem121Test {

    @Test
    public void test() {
        final int max = new Problem121().maxProfit(new int[]{7, 1, 5, 3, 6, 4});
        System.out.println(max);
    }

    @Test
    public void test2() {
        final int max = new Problem121().maxProfit(new int[]{7,6,4,3,1});
        System.out.println(max);
    }

}