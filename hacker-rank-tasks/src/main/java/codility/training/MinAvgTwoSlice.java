package codility.training;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * https://app.codility.com/programmers/lessons/5-prefix_sums/min_avg_two_slice/
 */
public class MinAvgTwoSlice {

    @Test
    public void test1() {
        assertEquals(1, testedObject().solution(new int[]{4, 2, 2, 5, 1, 5, 8}));
    }

    private static MinAvgTwoSlice testedObject() {
        return new MinAvgTwoSlice();
    }

    public int solution(final int[] array) {
        int start = 0;
        int stop = 1;
        double minAverageEndingHere = ((double) (array[start] + array[stop])) / 2;
        double minAverageSoFar = minAverageEndingHere;
        for (int i = 2; i < array.length; i++) {
            final int element = array[i];
            if (element < minAverageEndingHere) {
                final double sum = minAverageEndingHere * (stop - start + 1);
//                minAverageEndingHere
            }
//            minAverageEndingHere = Math.min();
        }
        return 0;
    }
}
