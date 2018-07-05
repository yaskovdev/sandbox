package days14.string;

import org.junit.Test;

import java.util.Arrays;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

/**
 * 1.1 (page 90)
 */
public class HasAllUnique {

    @Test
    public void test1() {
        assertFalse(hasAllUnique("abbdef"));
    }

    @Test
    public void test2() {
        assertTrue(hasAllUnique(""));
    }

    @Test
    public void test3() {
        assertTrue(hasAllUnique("Ы"));
    }

    @Test
    public void test4() {
        assertFalse(hasAllUnique("ЫЫ"));
    }

    private static boolean hasAllUnique(final String s) {
        final char[] chars = s.toCharArray();
        Arrays.sort(chars);
        for (int i = 0; i < chars.length - 1; i++) {
            if (chars[i] == chars[i + 1]) {
                return false;
            }
        }
        return true;
    }
}
