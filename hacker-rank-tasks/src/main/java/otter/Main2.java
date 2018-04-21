package otter;

import java.util.Scanner;
import java.util.TreeMap;

public class Main2 {

    private final static TreeMap<Integer, String> MAP = new TreeMap<>();

    static {
        MAP.put(1000, "M");
        MAP.put(900, "CM");
        MAP.put(500, "D");
        MAP.put(400, "CD");
        MAP.put(100, "C");
        MAP.put(90, "XC");
        MAP.put(50, "L");
        MAP.put(40, "XL");
        MAP.put(10, "X");
        MAP.put(9, "IX");
        MAP.put(5, "V");
        MAP.put(4, "IV");
        MAP.put(1, "I");
    }

    public static void main(final String[] args) {
        final Scanner scanner = new Scanner(System.in);
        while (true) {
            final String line = scanner.nextLine();
            if (line.isEmpty()) {
                return;
            } else {
                System.out.println(solution(Integer.parseInt(line)));
            }
        }
    }

    private static String solution(final int number) {
        final int floorKey = MAP.floorKey(number);
        return number == floorKey ? MAP.get(number) : MAP.get(floorKey) + solution(number - floorKey);
    }
}
