package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * 1.3 (page 90)
 */
public class Urlify {

    @Test
    public void test0() {
        assertEquals("Mr%20John%20Smith", urlify("Mr John Smith    ", 13));
    }

    @Test
    public void test1() {
        assertEquals("ab%20c%20", urlify("ab c     ", 5));
    }

    private String urlify(String string, int length) {
        final char[] s = string.toCharArray();
        for (int i = 0; i < length; i++) {
            if (s[i] == ' ') {
                for (int j = length - 1; j >= i + 1; j--) {
                    s[j + 2] = s[j];
                }
                s[i] = '%';
                s[i + 1] = '2';
                s[i + 2] = '0';
                i += 2;
                length += 2;
            }
        }
        return new String(s);
    }
}
