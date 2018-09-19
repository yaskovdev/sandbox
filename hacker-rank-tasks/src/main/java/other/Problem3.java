package other;

import java.util.HashSet;
import java.util.Set;

class Problem3 {

    int lengthOfLongestSubstring(String s) {
        String result = "";
        for (int i = 0; i < s.length(); i++) {
            for (int j = i; j < s.length(); j++) {
                final String candidate = s.substring(i, j + 1);
                if (hasNoRepeatingCharacters(candidate)) {
                    if (candidate.length() > result.length()) {
                        result = candidate;
                    }
                }
            }
        }
        return result.length();
    }

    private boolean hasNoRepeatingCharacters(final String string) {
        final Set<Character> set = new HashSet<>();
        final char[] array = string.toCharArray();
        for (final char c : array) {
            set.add(c);
        }
        return set.size() == array.length;
    }
}
