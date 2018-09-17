package other;

import java.util.Arrays;
import java.util.Collections;
import java.util.List;

class Problem680 {

    boolean validPalindrome(final String s) {
        final List<Integer> indexes = valid(s);
        if (indexes.isEmpty()) {
            return true;
        } else {
            final Integer i = indexes.get(0);
            final Integer j = indexes.get(1);
            return valid(s.substring(0, i) + s.substring(i + 1)).isEmpty() || valid(s.substring(0, j) + s.substring(j + 1)).isEmpty();
        }
    }

    private List<Integer> valid(String string) {
        for (int i = 0; i < string.length() / 2; i++) {
            if (string.charAt(i) != string.charAt(string.length() - i - 1)) {
                return Arrays.asList(i, string.length() - i - 1);
            }
        }
        return Collections.emptyList();
    }
}
