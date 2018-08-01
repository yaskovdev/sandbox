package days14.string;

import static java.util.Arrays.sort;

/**
 * 1.4 (page 91)
 */
public class PalindromePermutation {

    boolean solve(final String string) {
        final char[] chars = string.toLowerCase().replaceAll(" ", "").toCharArray();
        sort(chars);
        boolean uniqueMet = chars.length % 2 == 0;
        for (int i = 0; i < chars.length - 1; i += 2) {
            if (chars[i] != chars[i + 1]) {
                if (uniqueMet) {
                    return false;
                } else {
                    uniqueMet = true;
                    i++;
                }
            }
        }
        return true;
    }
}
