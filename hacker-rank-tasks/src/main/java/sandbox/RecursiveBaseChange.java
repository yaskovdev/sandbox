package sandbox;

import java.util.Scanner;

public class RecursiveBaseChange {

    public static void main(final String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int input = scanner.nextInt();
        final int base = scanner.nextInt();
        System.out.println(convert(input, base));
    }

    private static String convert(final int input, final int base) {
        final int lastDigit = input % base;
        return input >= base ? convert(input / base, base) + lastDigit : "" + lastDigit;
    }
}
