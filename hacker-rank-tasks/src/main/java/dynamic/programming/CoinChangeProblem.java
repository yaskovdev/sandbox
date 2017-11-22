package dynamic.programming;

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class CoinChangeProblem {

    public static void main(String[] args) {
        final Scanner in = new Scanner(System.in);
        final int amount = in.nextInt();
        final int numberOfCoins = in.nextInt();
        final Long[] coins = new Long[numberOfCoins];
        for (int i = 0; i < numberOfCoins; i++) {
            coins[i] = in.nextLong();
        }
        final long ways = ways(amount, Arrays.asList(coins));
        System.out.println(ways);
    }

    private static long ways(final long amount, final List<Long> coins) {
        if (amount < 0 || coins.isEmpty()) {
            return 0;
        } else if (amount == 0) {
            return 1;
        } else {
            final long coin = head(coins);
            return ways(amount - coin, coins) + ways(amount, tail(coins));
        }
    }

    private static long head(final List<Long> list) {
        return list.get(0);
    }

    private static List<Long> tail(final List<Long> list) {
        return list.subList(1, list.size());
    }
}
