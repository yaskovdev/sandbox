package other;

import java.util.HashMap;
import java.util.Map;

public class Problem237 {

    private final Map<Integer, String> groupToName = groupToName();
    private final Map<Integer, String> map = map();
    private final Map<Integer, String> map2 = map2();

    public String solve(int input) {
        final StringBuilder result = new StringBuilder();
        int number = input;
        int group = 0;
        while (number > 0) {
            int last = number % 1000;
            result.insert(0, " ").insert(0, name(last, group));
            number /= 1000;
            group += 1;
        }
        return result.toString();
    }

    private String name(int input, int group) {
        if (input < 100) {
            final int mod = input % 100;
            return nameLessThan100(input) + " " +  (mod == 0 ? "" : groupToName.get(group));
        } else {
            final int mod = input % 100;
            return name100(input / 100) + nameLessThan100(mod) + " " + (mod == 0 ? "" : groupToName.get(group));
        }
    }

    private String name100(int i) {
        return name10(i) + " Hundred ";
    }

    private String name10(int i) {
        return map.get(i);
    }

    private String nameLessThan100(int input) {
        if (input == 0) {
            return "";
        }
        if (input <= 19) {
            return map.get(input);
        }
        return nameDec(input / 10) + " " + name10(input % 10);
    }

    private String nameDec(int i) {
        return map2.get(i);
    }

    private static Map<Integer, String> groupToName() {
        final Map<Integer, String> groupToName = new HashMap<>();
        groupToName.put(0, "");
        groupToName.put(1, "Thousand");
        groupToName.put(2, "Million");
        groupToName.put(3, "Billion");
        groupToName.put(4, "Trillion");
        return groupToName;
    }

    private static Map<Integer, String> map() {
        final Map<Integer, String> map = new HashMap<>();
        map.put(1, "One");
        map.put(2, "Two");
        map.put(3, "Three");
        map.put(4, "Four");
        map.put(5, "Five");
        map.put(6, "Six");
        map.put(7, "Seven");
        map.put(8, "Eight");
        map.put(9, "Nine");
        map.put(10, "Ten");
        map.put(11, "Eleven");
        map.put(12, "Twelve");
        map.put(13, "Thirteen");
        map.put(14, "Fourteen");
        map.put(15, "Fifteen");
        map.put(16, "Sixteen");
        map.put(17, "Seventeen");
        map.put(18, "Eighteen");
        map.put(19, "Nineteen");
        return map;
    }

    private static Map<Integer, String> map2() {
        final Map<Integer, String> map2 = new HashMap<>();
        map2.put(2, "Twenty");
        map2.put(3, "Thirty");
        map2.put(4, "Forty");
        map2.put(5, "Fifty");
        map2.put(6, "Sixty");
        map2.put(7, "Seventy");
        map2.put(8, "Eighty");
        map2.put(9, "Ninety");
        return map2;
    }
}
