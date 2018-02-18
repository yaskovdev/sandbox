package codility2;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public class Third {

    public static void main(final String[] args) {
        System.out.println(new Third().solution(new int[]{1, 5, 4, 9, 8, 7, 12, 13, 14}));
        System.out.println(new Third().solution(new int[]{4, 3, 2, 6, 1}));
        System.out.println(new Third().solution(new int[]{4}));
    }

    public int solution(final int[] array) {
        final int[] sorted = new int[array.length];
        System.arraycopy(array, 0, sorted, 0, array.length);
        Arrays.sort(sorted);

        int result = 0;
        final Set<Integer> accumulator1 = new HashSet<>();
        final Set<Integer> accumulator2 = new HashSet<>();
        for (int i = 0; i < array.length; i++) {
            accumulator1.add(array[i]);
            accumulator2.add(sorted[i]);
            if (accumulator1.equals(accumulator2)) {
                result++;
                accumulator1.clear();
                accumulator2.clear();
            }
        }
        return result;
    }
}
