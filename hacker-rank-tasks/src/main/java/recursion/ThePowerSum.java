package recursion;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class ThePowerSum {

    public static void main(final String[] args) {
//        final Scanner scanner = new Scanner(System.in);
//        final int number = scanner.nextInt();
//        final int power = scanner.nextInt();
        System.out.println(solve(10, Collections.<Integer>emptyList()));
    }

    private static int solve(final int number, final int power) {
        return squrt(number);
    }

    private static int squrt(final int number) {
        return (int) Math.pow(number, 1.0 / 2);
    }

    private static boolean solve(final int number, final List<Integer> elements) {
        if (number == 0) {
            return true;
        } else if (number == 1) {
            return !elements.contains(number);
        } else {
            final int m = squrt(number);
            return solve(number - (int) Math.pow(m, 2), concat(elements, m));
        }
    }

    private static List<Integer> concat(final List<Integer> elements, final int m) {
        final List<Integer> result = new ArrayList<>(elements);
        result.add(m);
        return result;
    }
}
