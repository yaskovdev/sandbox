package codility.training;

import org.junit.Test;

import java.util.HashSet;
import java.util.Set;

import static org.junit.Assert.assertEquals;

/**
 * https://app.codility.com/programmers/lessons/6-sorting/distinct/
 */
public class Distinct {

    @Test
    public void test1() {
        assertEquals(3, testedObject().solution(new int[]{2, 2, 1, 3, 1, 1}));
    }

    private static Distinct testedObject() {
        return new Distinct();
    }

    public int solution(final int[] array) {
        final Set<Integer> set = new HashSet<>(array.length);
        for (final int element : array) {
            set.add(element);
        }
        return set.size();
    }
}
