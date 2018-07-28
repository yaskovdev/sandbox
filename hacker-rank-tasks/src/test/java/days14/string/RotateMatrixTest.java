package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;

public class RotateMatrixTest {

    @Test
    public void test1() {
        final int[][] matrix = {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
        };
        new RotateMatrix().rotate(matrix);
        assertArrayEquals(new int[]{7, 4, 1}, matrix[0]);
        assertArrayEquals(new int[]{8, 5, 2}, matrix[1]);
        assertArrayEquals(new int[]{9, 6, 3}, matrix[2]);
    }

    @Test
    public void test2() {
        final int[][] matrix = {
                {1}
        };
        new RotateMatrix().rotate(matrix);
        assertArrayEquals(new int[]{1}, matrix[0]);
    }
}
