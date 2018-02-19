package codility.training;

import java.util.Arrays;

/**
 * https://app.codility.com/programmers/lessons/4-counting_elements/frog_river_one/
 */
public class FrogRiverOne {

    public static void main(final String[] args) {
        System.out.println(new FrogRiverOne().solution(5, new int[]{1, 3, 1, 4, 2, 3, 5, 4}));
    }

    public int solution(final int x, final int[] array) {
        final boolean[] river = new boolean[x];
        Arrays.fill(river, false);
        int covered = 0;

        for (int second = 0; second < array.length; second++) {
            int position = array[second];
            if (!river[position - 1]) {
                covered++;
                river[position - 1] = true;
            }
            if (covered == river.length) {
                return second;
            }

        }
        return -1;
    }
}
