package warmup;

import java.util.Scanner;

public class Solution {

    public static void main(String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int size = scanner.nextInt();
        final int[][] matrix = new int[size][size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                matrix[i][j] = scanner.nextInt();
            }
        }
        System.out.println(abs(primaryDiagonalSum(matrix) - secondaryDiagonalSum(matrix)));
    }

    private static int abs(final int value) {
        return value < 0 ? -value : value;
    }

    private static int primaryDiagonalSum(final int[][] matrix) {
        int sum = 0;
        for (int i = 0; i < matrix.length; i++) {
            sum += matrix[i][i];
        }
        return sum;
    }

    private static int secondaryDiagonalSum(final int[][] matrix) {
        int sum = 0;
        for (int i = 0; i < matrix.length; i++) {
            sum += matrix[matrix.length - i - 1][i];
        }
        return sum;
    }

    private static void printMatrix(final int[][] matrix) {
        for (final int[] row : matrix) {
            for (int j = 0; j < matrix.length; j++) {
                System.out.print(row[j] + " ");
            }
            System.out.println();
        }
    }
}
