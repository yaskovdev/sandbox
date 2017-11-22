package dynamic.programming;

import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Objects;
import java.util.Scanner;

public class CoinChangeProblem {

    private static final Map<Key, Long> cache = new HashMap<>();

    private static class Key {

        final long amount;
        final List<Long> coins;

        private Key(final long amount, final List<Long> coins) {
            this.amount = amount;
            this.coins = coins;
        }

        @Override
        public boolean equals(final Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;
            final Key key = (Key) o;
            return amount == key.amount &&
                    Objects.equals(coins, key.coins);
        }

        @Override
        public int hashCode() {
            return Objects.hash(amount, coins);
        }
    }

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
        final Key key = new Key(amount, coins);
        final Long cachedWays = cache.get(key);
        if (cachedWays == null) {
            if (amount < 0 || coins.isEmpty()) {
                return 0;
            } else if (amount == 0) {
                return 1;
            } else {
                final long coin = head(coins);
                final long ways = ways(amount - coin, coins) + ways(amount, tail(coins));
                cache.put(key, ways);
                return ways;
            }
        } else {
            return cachedWays;
        }
    }

    private static long head(final List<Long> list) {
        return list.get(0);
    }

    private static List<Long> tail(final List<Long> list) {
        return list.subList(1, list.size());
    }
}
