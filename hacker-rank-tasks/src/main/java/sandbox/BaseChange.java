package sandbox;

import java.util.Scanner;

public class BaseChange {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        final int input = scanner.nextInt();
        System.out.println(convert(input));
    }

    private static String convert(int input) {
        final StringBuilder builder = new StringBuilder();
        while (input > 0) {
            final int lastDigit = lastDigit(input);
            input = (input - lastDigit) / 2;
            builder.append(lastDigit);
        }
        return builder.reverse().toString();
    }

    private static int lastDigit(final int number) {
        return number % 2;
    }
}
