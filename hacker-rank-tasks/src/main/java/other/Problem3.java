package other;

import java.util.HashSet;
import java.util.Set;

class Problem3 {

    int lengthOfLongestSubstring(String s) {
        int i = 0;
        int j = 0;
        int record = 0;
        final Set<Character> buffer = new HashSet<>();
        while (true) {
            while (j < s.length() && !buffer.contains(s.charAt(j))) {
                buffer.add(s.charAt(j));
                j++;
            }
            record = Math.max(record, j - i);
            if (j == s.length()) {
                return record;
            }
            while (buffer.contains(s.charAt(j))) {
                buffer.remove(s.charAt(i));
                i++;
            }
        }
    }
}
