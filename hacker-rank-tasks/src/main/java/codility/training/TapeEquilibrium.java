package codility.training;

/**
 * https://app.codility.com/programmers/lessons/3-time_complexity/tape_equilibrium/
 */
public class TapeEquilibrium {

    public static void main(String[] args) {
        System.out.println(new TapeEquilibrium().solution(new int[]{1}));
        System.out.println(new TapeEquilibrium().solution(new int[]{3, 1, 2, 4, 3}));
        System.out.println(new TapeEquilibrium().solution(new int[]{-1000, 1000}));
    }

    public int solution(int[] array) {
        int split = 1;
        int sum1 = array[0];
        int sum2 = sum(array, split, array.length);

        int min = diff(sum1, sum2);

        for (int i = 1; i < array.length - 1; i++) {
            sum1 += array[i];
            sum2 -= array[i];
            int candidate = diff(sum1, sum2);
            if (candidate < min) {
                min = candidate;
            }
        }
        return min;
    }

    private int sum(int[] array, int from, int to) {
        int sum = 0;
        for (int i = from; i < to; i++) {
            sum += array[i];
        }
        return sum;
    }

    private int diff(int first, int second) {
        return Math.abs(second - first);
    }
}
