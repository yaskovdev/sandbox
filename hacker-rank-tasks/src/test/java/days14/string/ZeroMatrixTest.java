package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;

public class ZeroMatrixTest {

    @Test
    public void test1() {
        final Integer[][] matrix = {
                {1, 2, 3},
                {4, 0, 6}
        };
        new ZeroMatrix().zero(matrix);
        assertArrayEquals(new Integer[]{1, 0, 3}, matrix[0]);
        assertArrayEquals(new Integer[]{0, 0, 0}, matrix[1]);
    }

    @Test
    public void test2() {
        final Integer[][] matrix = {
                {1, 2, 3},
                {0, 0, 6}
        };
        new ZeroMatrix().zero(matrix);
        assertArrayEquals(new Integer[]{0, 0, 3}, matrix[0]);
        assertArrayEquals(new Integer[]{0, 0, 0}, matrix[1]);
    }

}