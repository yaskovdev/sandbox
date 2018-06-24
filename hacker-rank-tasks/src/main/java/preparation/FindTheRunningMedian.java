package preparation;

import org.junit.Test;

import java.util.Arrays;

import static java.util.Arrays.sort;

public class FindTheRunningMedian {

    @Test
    public void test0() {
        solution(new int[]{12, 4, 5, 3, 8, 7});
    }

    private static void solution(final int[] numbers) {
        for (int i = 0; i < numbers.length; i++) {
            final int[] sub = Arrays.copyOfRange(numbers, 0, i + 1);
            System.out.println(String.format("%.1f", median(sub)));
        }
    }

    private static double median(final int[] numbers) {
        sort(numbers);
        return numbers.length % 2 == 0 ? (numbers[numbers.length / 2 - 1] + numbers[numbers.length / 2]) / 2.0 : numbers[numbers.length / 2];
    }
}
