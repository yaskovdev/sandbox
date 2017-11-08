package sandbox;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

import static java.lang.Math.pow;

public class RecursiveBaseToDecimalChange {

    public static void main(final String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final String digits = scanner.next();
        final int base = scanner.nextInt();
        System.out.println(convert(digits, base));
    }

    private static Integer convert(final String digits, final int base) {
        return convert(toDigits(digits.toCharArray()), base, 0);
    }

    private static Integer convert(final List<Integer> digits, final int base, final int place) {
        final int size = digits.size();
        final int lastDigit = digits.get(size - 1);
        final int portion = (int) (lastDigit * pow(base, place));
        return size > 1 ? convert(digits.subList(0, size - 1), base, place + 1) + portion : portion;
    }

    private static List<Integer> toDigits(final char[] chars) {
        final List<Integer> result = new ArrayList<>(chars.length);
        for (final char character : chars) {
            result.add(Integer.valueOf(String.valueOf(character)));
        }
        return result;
    }
}
