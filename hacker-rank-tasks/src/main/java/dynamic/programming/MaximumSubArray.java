package dynamic.programming;

import java.util.Scanner;

public class MaximumSubArray {

    public static void main(final String args[]) throws Exception {
        final Scanner scanner = new Scanner(System.in);
        final int numberOfTestCases = scanner.nextInt();
        for (int i = 0; i < numberOfTestCases; i++) {
            final int size = scanner.nextInt();
            final int[] input = new int[size];
            for (int j = 0; j < size; j++) {
                input[j] = scanner.nextInt();
            }
            System.out.println(maxContiguousSubArray(input) + " " + maxNonContiguousSubArray(input));
        }
    }

    private static int maxContiguousSubArray(final int[] input) {
        int maxEndingHere = input[0];
        int max = input[0];
        for (int i = 1; i < input.length; i++) {
            maxEndingHere = Math.max(maxEndingHere + input[i], input[i]);
            max = Math.max(max, maxEndingHere);
        }
        return max;
    }

    private static int maxNonContiguousSubArray(final int[] input) {
        int sum = 0;
        int max = input[0];
        boolean wasNonNegativeElement = false;
        for (int element : input) {
            if (element >= 0) {
                sum += element;
                wasNonNegativeElement = true;
            }
            if (element > max) {
                max = element;
            }
        }
        return wasNonNegativeElement ? sum : max;
    }
}
