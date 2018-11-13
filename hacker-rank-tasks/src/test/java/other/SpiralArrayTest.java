package other;

import org.junit.Test;

public class SpiralArrayTest {

    @Test
    public void test_spiral() {
        final int[][] spiral = new SpiralArray().spiral(5);
        for (int i = 0; i < spiral.length; i++) {
            for (int j = 0; j < spiral[i].length; j++) {
                System.out.print(String.format("%4d", spiral[j][i]));
            }
            System.out.println();
        }
    }
}