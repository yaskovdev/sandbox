package days14.string;

/**
 * 1.7 (page 91)
 */
public class RotateMatrix {

    public void rotate(final int[][] matrix) {
        transpose(matrix);
        reverseRows(matrix);
    }

    private static void transpose(final int[][] matrix) {
        final int m = matrix.length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < m; j++) {
                if (i < j) {
                    swap(matrix, i, j, j, i);
                }
            }
        }
    }

    private static void reverseRows(final int[][] matrix) {
        final int m = matrix.length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < m / 2; j++) {
                swap(matrix, i, j, i, m - j - 1);
            }
        }
    }

    private static void swap(final int[][] matrix, final int i1, final int j1, final int i2, final int j2) {
        final int temp = matrix[i1][j1];
        matrix[i1][j1] = matrix[i2][j2];
        matrix[i2][j2] = temp;
    }
}
