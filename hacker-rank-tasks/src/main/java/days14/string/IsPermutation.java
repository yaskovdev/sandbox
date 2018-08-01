package days14.string;

import java.util.HashMap;
import java.util.Map;

import static java.util.Objects.isNull;

/**
 * 1.2 (page 90)
 */
public class IsPermutation {

    boolean solve(final String s1, final String s2) {
        return toMap(s1).equals(toMap(s2));
    }

    private static Map<Character, Integer> toMap(final String s) {
        final Map<Character, Integer> map = new HashMap<>(s.length());
        for (final char c : s.toCharArray()) {
            final Integer frequency = map.get(c);
            if (isNull(frequency)) {
                map.put(c, 1);
            } else {
                map.put(c, frequency + 1);
            }
        }
        return map;
    }
}
