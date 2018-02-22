package codility.training;

import org.junit.Test;

import java.util.Arrays;

import static org.junit.Assert.assertEquals;

/**
 * https://app.codility.com/programmers/lessons/6-sorting/triangle/
 */
public class Triangle {

    @Test
    public void test1() {
        assertEquals(1, testedObject().solution(new int[]{10, 2, 5, 1, 8, 20}));
    }

    private static Triangle testedObject() {
        return new Triangle();
    }

    public int solution(final int[] array) {
        Arrays.sort(array);
        for (int i = 0; i < array.length - 2; i++) {
            long a = array[i];
            long b = array[i + 1];
            long c = array[i + 2];
            if (a + b > c && b + c > a && c + a > b) {
                return 1;
            }
        }
        return 0;
    }
}
