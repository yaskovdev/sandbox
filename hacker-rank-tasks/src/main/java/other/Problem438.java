package other;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

class Problem438 {

    List<Integer> findAnagrams(final String s, final String p) {
        final List<Integer> result = new ArrayList<>();
        final Map<Character, Integer> requirement = charToFrequency(p);
        for (int i = 0; i <= s.length() - p.length(); i++) {
            final String window = s.substring(i, i + p.length());
            final Map<Character, Integer> charToFrequency = charToFrequency(window);
            if (requirement.equals(charToFrequency)) {
                result.add(i);
            }
        }
        return result;
    }

    private static Map<Character, Integer> charToFrequency(final String string) {
        final Map<Character, Integer> result = new HashMap<>();
        for (final char c : string.toCharArray()) {
            final Integer frequency = result.computeIfAbsent(c, k -> 0);
            result.put(c, frequency + 1);
        }
        return result;
    }
}
