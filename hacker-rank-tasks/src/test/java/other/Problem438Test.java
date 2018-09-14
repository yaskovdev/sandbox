package other;

import org.junit.Test;

import java.util.List;

import static java.util.Arrays.asList;
import static org.junit.Assert.assertEquals;

public class Problem438Test {

    @Test
    public void test() {
        final List<Integer> indexes = new Problem438().findAnagrams("cbaebabacd", "abc");
        assertEquals(asList(0, 6), indexes);
    }

    @Test
    public void test2() {
        final List<Integer> indexes = new Problem438().findAnagrams("abab", "ab");
        assertEquals(asList(0, 1, 2), indexes);
    }
}
