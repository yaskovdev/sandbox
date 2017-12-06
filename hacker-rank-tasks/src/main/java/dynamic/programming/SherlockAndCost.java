package dynamic.programming;

import java.util.Scanner;

/**
 * https://www.hackerrank.com/challenges/sherlock-and-cost/problem
 */
public class SherlockAndCost {

    public static void main(String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int numberOfTestCases = scanner.nextInt();
        for (int i = 0; i < numberOfTestCases; i++) {
            final int size = scanner.nextInt();
            final int[] array = new int[size];
            for (int j = 0; j < size; j++) {
                array[j] = scanner.nextInt();
            }
            System.out.println(solve(array));
        }
    }

    private static int solve(final int[] array) {
        return 0;
    }
}
