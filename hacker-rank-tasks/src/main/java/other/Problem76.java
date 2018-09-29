package other;

import java.util.HashMap;
import java.util.Map;

class Problem76 {

    String minWindow(String s, String t) {
        if (s.isEmpty()) {
            return "";
        } else {
            int l = 0;
            int r = 0;
            String output = "";
            while (true) {
                while (r < s.length() - 1 && !contains(s, l, r, t)) {
                    r++;
                }
                while (l <= r && contains(s, l, r, t)) {
                    l++;
                }
                if (l == 0) {
                    return output;
                }
                String candidate = s.substring(l - 1, r + 1);
                if (output.isEmpty() || candidate.length() < output.length()) {
                    output = candidate;
                }
                if (r == s.length() - 1) {
                    return output;
                }
            }
        }
    }

    private boolean contains(final String s, final int l, final int r, final String t) {
        final Map<Character, Integer> charToFreq = new HashMap<>();
        for (final char c : s.substring(l, r + 1).toCharArray()) {
            final Integer freq = charToFreq.computeIfAbsent(c, k -> 0);
            charToFreq.put(c, freq + 1);
        }
        for (final char c : t.toCharArray()) {
            final Integer freq = charToFreq.getOrDefault(c, 0);
            if (freq == 0) {
                return false;
            } else {
                charToFreq.put(c, freq - 1);
            }
        }
        return true;
    }
}
