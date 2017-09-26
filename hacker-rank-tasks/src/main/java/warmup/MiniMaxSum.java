package warmup;

import java.util.Scanner;

public class MiniMaxSum {

    public static void main(String[] args) {
        final Scanner in = new Scanner(System.in);
        long[] arr = new long[5];
        for (int i = 0; i < 5; i++) {
            arr[i] = in.nextLong();
        }

        long min = arr[0];
        long max = arr[0];
        long sum = 0;
        for (long number : arr) {
            if (number > max) {
                max = number;
            } else if (number < min) {
                min = number;
            }
            sum += number;
        }
        System.out.println((sum - max) + " " + (sum - min));
    }
}
