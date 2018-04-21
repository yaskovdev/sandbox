package otter;

import java.math.BigDecimal;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Main3 {

    private static final String SEPARATOR = " + ";

    private static final Map<Integer, String> COINS = new HashMap<>();

    static {
        COINS.put(1, "PENNY");
        COINS.put(5, "NICKEL");
        COINS.put(10, "DIME");
        COINS.put(25, "QUARTER");
        COINS.put(50, "HALF DOLLAR");
        COINS.put(100, "ONE");
        COINS.put(200, "TWO");
        COINS.put(500, "FIVE");
        COINS.put(1000, "TEN");
        COINS.put(2000, "TWENTY");
        COINS.put(5000, "FIFTY");
        COINS.put(10000, "ONE HUNDRED");
    }

    public static void main(final String[] args) {
        final Scanner scanner = new Scanner(System.in);
        while (true) {
            final String line = scanner.nextLine();
            if (line.isEmpty()) {
                return;
            } else {
                final String[] tokens = line.split(";");
                final int price = new BigDecimal(tokens[0]).multiply(new BigDecimal(100)).intValue();
                final int cash = new BigDecimal(tokens[1]).multiply(new BigDecimal(100)).intValue();
                System.out.println(solution(price, cash));
            }
        }
    }

    private static String solution(final int priceInPennies, final int cashInPennies) {
        if (cashInPennies < priceInPennies) {
            return "ERROR";
        } else if (cashInPennies == priceInPennies) {
            return "ZERO";
        } else {
            final int changeInPennies = cashInPennies - priceInPennies;
            final Map<Integer, Integer> map = makeChange(COINS.keySet().toArray(new Integer[COINS.size()]), changeInPennies);
            return format(map);
        }
    }

    private static String format(final Map<Integer, Integer> map) {
        final StringBuilder builder = new StringBuilder();
        for (final Map.Entry<Integer, Integer> entry : map.entrySet()) {
            builder.append(entry.getValue()).append(" x ").append(COINS.get(entry.getKey())).append(SEPARATOR);
        }
        return builder.toString().substring(0, builder.length() - SEPARATOR.length());
    }

    private static Map<Integer, Integer> makeChange(final Integer[] coins, final int price) {
        int amount = price;
        Arrays.sort(coins);

        int index = coins.length - 1;

        final Map<Integer, Integer> map = new HashMap<>();

        while (amount > 0) {
            if (coins[index] <= amount) {
                int numberOfCoins = 1;
                if (map.containsKey(coins[index])) {
                    numberOfCoins = map.get(coins[index]);
                    numberOfCoins = numberOfCoins + 1;
                }
                map.put(coins[index], numberOfCoins);
                amount = amount - coins[index];
            } else {
                index--;
            }
        }
        return map;
    }
}
