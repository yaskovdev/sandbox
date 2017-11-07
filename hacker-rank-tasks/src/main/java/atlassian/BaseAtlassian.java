package atlassian;

import java.util.HashMap;
import java.util.Map;

public class BaseAtlassian {

    private static final int BASE = 7;

    private static Map<Integer, Character> map = map();

    public static void main(String[] args) {
        System.out.println(base(7792875));
    }

    private static String base(final int number) {
        int n = number;
        final StringBuilder result = new StringBuilder();
        while (n > 0) {
            int value = n % BASE;
            result.append(letterFor(value));
            n = (n - value) / BASE;
        }
        return result.reverse().toString();
    }

    private static char letterFor(int value) {
        return map.get(value);
    }

    private static Map<Integer, Character> map() {
        final HashMap<Integer, Character> map = new HashMap<>();
        map.put(0, '0');
        map.put(1, 'a');
        map.put(2, 't');
        map.put(3, 'l');
        map.put(4, 's');
        map.put(5, 'i');
        map.put(6, 'n');
        return map;
    }
}
