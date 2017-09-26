package warmup;

import java.util.Arrays;
import java.util.Scanner;

public class BirthdayCakeCandles {
    private static int birthdayCakeCandles(int n, int[] heights) {
        Arrays.sort(heights);
        int result = 0;
        int max = heights[heights.length - 1];
        for (int i = heights.length - 1; i >= 0 && heights[i] == max; i--) {
            result++;
        }
        return result;
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int[] ar = new int[n];
        for (int ar_i = 0; ar_i < n; ar_i++) {
            ar[ar_i] = in.nextInt();
        }
        int result = birthdayCakeCandles(n, ar);
        System.out.println(result);
    }
}
