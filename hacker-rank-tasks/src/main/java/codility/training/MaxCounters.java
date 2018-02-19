package codility.training;

import org.junit.Test;

import java.util.Arrays;

import static org.junit.Assert.assertArrayEquals;

/**
 * https://app.codility.com/programmers/lessons/4-counting_elements/max_counters/
 */
public class MaxCounters {

    @Test
    public void test1() {
        assertArrayEquals(new int[]{3, 2, 2, 4, 2}, testedObject().solution(5, new int[]{3, 4, 4, 6, 1, 4, 4}));
    }

    private static MaxCounters testedObject() {
        return new MaxCounters();
    }

    public int[] solution(final int n, final int[] operations) {
        final int[] counters = new int[n];
        Arrays.fill(counters, 0);
        int max = 0;
        int lastAppliedMax = 0;

        for (final int operation : operations) {
            if (1 <= operation && operation <= n) {
                final int index = operation - 1;
                counters[index] = Math.max(lastAppliedMax, counters[index]) + 1;
                if (counters[index] > max) {
                    max = counters[index];
                }
            } else if (operation == n + 1) {
                lastAppliedMax = max;
            }
        }

        for (int i = 0; i < counters.length; i++) {
            if (counters[i] < lastAppliedMax) {
                counters[i] = lastAppliedMax;
            }
        }
        return counters;
    }
}
