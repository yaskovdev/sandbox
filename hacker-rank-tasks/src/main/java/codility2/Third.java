package codility2;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

/**
 * We have an array. We are allowed to do 2 things with it. We are allowed to divide it into groups. We are allowed to
 * swap elements inside a group. Note: we are not allowed to swap groups. We want to have the array sorted. What is the
 * maximum number of groups that allows us to do it?
 */
public class Third {

    public static void main(final String[] args) {
        System.out.println(new Third().solution(new int[]{1, 5, 4, 9, 8, 7, 12, 13, 14}));
        System.out.println(new Third().solution(new int[]{4, 3, 2, 6, 1}));
        System.out.println(new Third().solution(new int[]{4}));
        System.out.println(new Third().solution(new int[]{1, 2, 4, 3}));
        System.out.println(new Third().solution(new int[]{1, 2, 3}));
        System.out.println(new Third().solution(new int[]{3, 2, 1}));
        System.out.println(new Third().solution(new int[]{3, 1, 5}));
    }

    public int solution(final int[] array) {
        final int[] sorted = new int[array.length];
        System.arraycopy(array, 0, sorted, 0, array.length);
        Arrays.sort(sorted);

        int result = 0;
        final Set<Integer> set = new HashSet<>();
        for (int i = 0; i < array.length; i++) {
            removeIfContainsAddOtherwise(set, array[i]);
            removeIfContainsAddOtherwise(set, sorted[i]);
            if (set.isEmpty()) {
                result++;
            }
        }
        return result;
    }

    private static void removeIfContainsAddOtherwise(final Set<Integer> set, final int element) {
        if (set.contains(element)) {
            set.remove(element);
        } else {
            set.add(element);
        }
    }
}
