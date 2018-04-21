package codility.training;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * https://app.codility.com/programmers/lessons/5-prefix_sums/passing_cars/
 */
public class PassingCars {

    @Test
    public void test1() {
        assertEquals(5, testedObject().solution(new int[]{0, 1, 0, 1, 1}));
    }

    private static PassingCars testedObject() {
        return new PassingCars();
    }

    public int solution(final int[] cars) {
        int goToEast = 0;
        for (final int car : cars) {
            goToEast += car;
        }

        int result = 0;
        for (final int car : cars) {
            if (car == 0) {
                result += goToEast;
            } else {
                goToEast -= 1;
            }

            if (result > 1_000_000_000) {
                return -1;
            }
        }

        return result;
    }
}
