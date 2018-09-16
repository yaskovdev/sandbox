package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

class Problem438 {

    List<Integer> findAnagrams(final String s, final String p) {
        if (p.length() > s.length()) {
            return Collections.emptyList();
        }
        final List<Integer> result = new ArrayList<>();
        final Map<Character, Integer> requirement = charToFrequency(p);
        final Map<Character, Integer> charToFrequency = charToFrequency(s.substring(0, p.length()));
        if (charToFrequency.equals(requirement)) {
            result.add(0);
        }
        if (s.length() > 1) {
            for (int i = 1; i <= s.length() - p.length(); i++) {
                char toRemove = s.charAt(i - 1);
                char toAdd = s.charAt(i + p.length() - 1);
                final Integer frequency = charToFrequency.computeIfAbsent(toAdd, k -> 0);
                charToFrequency.put(toAdd, frequency + 1);
                charToFrequency.put(toRemove, charToFrequency.get(toRemove) - 1);
                if (charToFrequency.get(toRemove) == 0) {
                    charToFrequency.remove(toRemove);
                }
                if (charToFrequency.equals(requirement)) {
                    result.add(i);
                }
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
