package atlassian;

/**
 * Created by sergei.iaskov on 12/1/2017.
 */
public class Main {
    public static void main(String[] args) {
        int[][] matrix = {{0, 0}, {0, 0}};
        fill(matrix, 1);
        print(matrix);
    }

    private static void print(int[][] matrix) {
        for (int[] row : matrix) {
            for (int val : row) {
                System.out.print(val + " ");
            }
            System.out.println();
        }
    }

    private static void fill(int[][] matrix, int quarter) {
        if (matrix.length == 2) {
            if (quarter == 1) {
                matrix[0][0] = quarter;
            } else if (quarter == 2) {
                matrix[0][1] = quarter;
            } else if (quarter == 3) {
                matrix[1][0] = quarter;
            } else if (quarter == 4) {
                matrix[1][1] = quarter;
            } else {
                throw new RuntimeException("oops");
            }
        } else {

        }
    }
}
