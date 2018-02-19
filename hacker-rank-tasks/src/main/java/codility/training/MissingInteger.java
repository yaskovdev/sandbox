package codility.training;

import org.junit.Test;

import java.util.HashSet;
import java.util.Set;

import static org.junit.Assert.assertEquals;

/**
 * https://app.codility.com/programmers/lessons/4-counting_elements/missing_integer/
 */
public class MissingInteger {

    @Test
    public void test1() {
        assertEquals(5, testedObject().solution(new int[]{1, 3, 6, 4, 1, 2}));
    }

    @Test
    public void test2() {
        assertEquals(4, testedObject().solution(new int[]{1, 2, 3}));
    }

    @Test
    public void test3() {
        assertEquals(1, testedObject().solution(new int[]{-1, -3}));
    }

    private static MissingInteger testedObject() {
        return new MissingInteger();
    }

    public int solution(final int[] array) {
        final Set<Integer> set = new HashSet<>(array.length);
        for (int element : array) {
            set.add(element);
        }
        for (int i = 1; ; i++) {
            if (!set.contains(i)) {
                return i;
            }
        }
    }
}
