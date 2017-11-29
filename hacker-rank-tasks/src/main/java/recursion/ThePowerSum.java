package recursion;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;

public class ThePowerSum {

    public static void main(final String[] args) {
//        final Scanner scanner = new Scanner(System.in);
//        final int number = scanner.nextInt();
//        final int power = scanner.nextInt();
        System.out.println(ways(100, Collections.<Integer>emptyList()));
    }

    private static int solve(final int number, final int power) {
        return sqrt(number);
    }

    private static int sq(final int number) {
        return (int) Math.pow(number, 2);
    }

    private static int sqrt(final int number) {
        return (int) Math.pow(number, 1.0 / 2);
    }

    private static int ways(final int number, final List<Integer> elements) {
        if (hasDuplicates(elements)) {
            return 0;
        } else if (number == 0) {
            return 1;
        } else {
            final int m = sqrt(number);
            return ways(number - sq(m), concat(elements, m)) + ways(number - sq(m - 1), concat(elements, m - 1));
        }
    }

    private static List<Integer> concat(final List<Integer> elements, final int m) {
        final List<Integer> result = new ArrayList<>(elements);
        result.add(m);
        return result;
    }

    private static boolean hasDuplicates(final List<Integer> list) {
        return new HashSet<>(list).size() < list.size();
    }
}
