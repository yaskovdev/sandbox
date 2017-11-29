package recursion;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

/**
 * https://www.hackerrank.com/challenges/the-power-sum/problem
 */
public class ThePowerSum {

    public static void main(final String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int number = scanner.nextInt();
        final int power = scanner.nextInt();
        System.out.println(solve(number, power));
    }

    private static long solve(final long number, final long power) {
        final List<Long> coins = new ArrayList<>();
        for (int i = 1; i <= root(number, power); i++) {
            coins.add(pow(i, power));
        }
        return ways(number, coins);
    }

    private static long ways(final long number, final List<Long> coins) {
        if (number == 0) {
            return 1;
        } else if (number < 0 || coins.isEmpty()) {
            return 0;
        } else {
            return ways(number - head(coins), tail(coins)) + ways(number, tail(coins));
        }
    }

    private static long root(final long number, final long power) {
        return (long) Math.pow(number, 1.0 / power);
    }

    private static long pow(final long number, final long power) {
        return (long) Math.pow(number, power);
    }

    private static long head(final List<Long> list) {
        return list.get(0);
    }

    private static List<Long> tail(final List<Long> list) {
        return list.subList(1, list.size());
    }
}
