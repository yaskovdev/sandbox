package other;

import java.util.HashMap;
import java.util.Map;

class Problem91 {

    private final Map<String, Integer> cache = new HashMap<>();

    int numDecodings(final String s) {
        if (cache.containsKey(s)) {
            return cache.get(s);
        } else if (s.startsWith("0")) {
            return 0;
        } else if (s.isEmpty()) {
            return 1;
        } else if (s.length() == 1) {
            return 1;
        } else {
            String first2 = s.substring(0, 2);
            String second1 = s.substring(1, 2);
            if (Integer.parseInt(first2) <= 26) {
                if (Integer.parseInt(second1) == 0) {
                    final int number = numDecodings(s.substring(2));
                    cache.put(s, number);
                    return number;
                } else {
                    final int number = numDecodings(s.substring(1)) + numDecodings(s.substring(2));
                    cache.put(s, number);
                    return number;
                }
            } else {
                final int number = numDecodings(s.substring(1));
                cache.put(s, number);
                return number;
            }
        }
    }
}
