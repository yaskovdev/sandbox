package qualified;

import java.util.HashMap;
import java.util.Map;

public class Second {

    private static final Map<Integer, String> lastDigitToEnding = new HashMap<>();

    static {
        lastDigitToEnding.put(0, "th");
        lastDigitToEnding.put(1, "st");
        lastDigitToEnding.put(2, "nd");
        lastDigitToEnding.put(3, "rd");
        lastDigitToEnding.put(4, "th");
        lastDigitToEnding.put(5, "th");
        lastDigitToEnding.put(6, "th");
        lastDigitToEnding.put(7, "th");
        lastDigitToEnding.put(8, "th");
        lastDigitToEnding.put(9, "th");
    }

    public static void main(String[] args) {
        int[] edgeCases = {0, 1, 2, 3, 4, 5, 10, 11, 12, 13, 14, 20, 21, 22, 23, 24, 100, 101, 102, 103, 104, 111, 112, 113, 114, 10000};
        for (final Integer edgeCase : edgeCases) {
            System.out.println(new Second().numberToOrdinal(edgeCase));
        }
    }

    public String numberToOrdinal(final Integer number) {
        if (number == 0) {
            return "0";
        } else {
            final int mod100 = number % 100;
            if (mod100 == 11 || mod100 == 12 || mod100 == 13) {
                return number + "th";
            } else {
                final int mod10 = number % 10;
                return number + lastDigitToEnding.get(mod10);
            }
        }
    }
}
