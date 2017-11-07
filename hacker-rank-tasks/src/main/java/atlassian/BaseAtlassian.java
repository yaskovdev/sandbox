package atlassian;

import java.util.HashMap;
import java.util.Map;

public class BaseAtlassian {

    private static final int BASE = 7;

    private static Map<Long, Character> map = map();

    public static void main(String[] args) {
        System.out.println(base(7792875));
        System.out.println(base(1000000000000L));
    }

    private static String base(final long number) {
        long n = number;
        final StringBuilder result = new StringBuilder();
        while (n > 0) {
            long value = n % BASE;
            result.append(letterFor(value));
            n = (n - value) / BASE;
        }
        return result.reverse().toString();
    }

    private static char letterFor(long value) {
        return map.get(value);
    }

    private static Map<Long, Character> map() {
        final HashMap<Long, Character> map = new HashMap<>();
        map.put(0L, '0');
        map.put(1L, 'a');
        map.put(2L, 't');
        map.put(3L, 'l');
        map.put(4L, 's');
        map.put(5L, 'i');
        map.put(6L, 'n');
        return map;
    }
}
