package recursion;

import java.util.Scanner;

/**
 * https://www.hackerrank.com/challenges/recursive-digit-sum/problem
 */
public class RecursiveDigitSum {

    public static void main(String[] args) {
        final Scanner in = new Scanner(System.in);
        final String n = in.next();
        final int k = in.nextInt();
        final int result = superDigit(n, k);
        System.out.println(result);
        in.close();
    }

    private static int superDigit(final String n, final int k) {
        final StringBuilder builder = new StringBuilder();
        for (int i = 0; i < k; i++) {
            builder.append(n);
        }
        final String number = builder.toString();
        return Integer.parseInt(superDigit(number));
    }

    private static String superDigit(final String number) {
        return number.length() == 1 ? number : superDigit(sumOfDigits(number));
    }

    private static String sumOfDigits(final String number) {
        int result = 0;
        final char[] chars = number.toCharArray();
        for (final char theChar : chars) {
            result += Integer.parseInt(Character.toString(theChar));
        }
        return "" + result;
    }
}
