package org.example;

public class Main {

    public static void main(String[] args) {
        System.out.println("6 / 2 + 1 = " + divide(6, 2).flatMap(result -> new Just<>(result + 1)));
        System.out.println("6 / 0 + 1 = " + divide(6, 0).flatMap(result -> new Just<>(result + 1)));
    }

    @SuppressWarnings("SameParameterValue")
    private static Maybe<Integer> divide(int dividend, int divisor) {
        return divisor == 0 ? new Nothing<>() : new Just<>(dividend / divisor);
    }
}