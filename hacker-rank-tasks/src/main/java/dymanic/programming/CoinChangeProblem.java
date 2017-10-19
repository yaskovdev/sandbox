package dymanic.programming;

import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class CoinChangeProblem {

    public static void main(String[] args) {
        final Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int m = in.nextInt();
        Long[] c = new Long[m];
        for (int i = 0; i < m; i++) {
            c[i] = in.nextLong();
        }
        long ways = ways(n, Arrays.asList(c));
        System.out.println(ways);
    }

    private static long ways(final long n, final List<Long> coins) {
        if (coins.isEmpty()) {
            return 0;
        } else if (n == 0) {
            return 1;
        } else {
            final long coin = head(coins);
            return (n % coin == 0 ? 1 : 0) + ways(n - coin, tail(coins));
        }
    }

    private static long head(final List<Long> list) {
        return list.get(0);
    }

    private static List<Long> tail(final List<Long> list) {
        return list.subList(1, list.size());
    }
}
