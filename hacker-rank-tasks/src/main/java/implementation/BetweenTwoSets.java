package implementation;

import java.util.Scanner;

public class BetweenTwoSets {

    private static int min(int... xs) {
        int result = xs[0];
        for (final int x : xs) {
            if (x < result) {
                result = x;
            }
        }
        return result;
    }

    private static int max(int... xs) {
        int result = xs[0];
        for (final int x : xs) {
            if (x > result) {
                result = x;
            }
        }
        return result;
    }

    private static boolean allAreFactorsOf(final int[] xs, final int y) {
        for (final int x : xs) {
            if (!isFactorOf(x, y)) {
                return false;
            }
        }
        return true;
    }

    private static boolean isFactorOfAll(final int x, final int[] xs) {
        for (int i : xs) {
            if (!isFactorOf(x, i)) {
                return false;
            }
        }
        return true;
    }

    private static boolean isFactorOf(final int x, final int y) {
        return y % x == 0;
    }

    private static int getTotalX(int[] a, int[] b) {
        int total = 0;
        for (int x = min(a); x <= max(b); x++) {
            if (allAreFactorsOf(a, x) && isFactorOfAll(x, b)) {
                total++;
            }
        }
        return total;
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int m = in.nextInt();
        int[] a = new int[n];
        for (int a_i = 0; a_i < n; a_i++) {
            a[a_i] = in.nextInt();
        }
        int[] b = new int[m];
        for (int b_i = 0; b_i < m; b_i++) {
            b[b_i] = in.nextInt();
        }
        int total = getTotalX(a, b);
        System.out.println(total);
        in.close();
    }
}
