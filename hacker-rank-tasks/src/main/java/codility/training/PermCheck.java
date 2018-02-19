package codility.training;

import java.util.HashMap;
import java.util.Map;

/**
 * https://app.codility.com/programmers/lessons/4-counting_elements/perm_check/
 */
public class PermCheck {

    public static void main(final String[] args) {
        System.out.println(new PermCheck().solution(new int[]{4, 1, 3, 2}));
        System.out.println(new PermCheck().solution(new int[]{4, 1, 3}));
        System.out.println(new PermCheck().solution(new int[]{9, 5, 7, 3, 2, 7, 3, 1, 10, 8}));
    }

    public int solution(final int[] array) {
        final int length = array.length;

        for (final int element : array) {
            if (element > length) {
                return 0;
            }
        }

        final Map<Integer, Boolean> map = new HashMap<>(length);

        for (final int element : array) {
            if (map.containsKey(element)) {
                return 0;
            } else {
                map.put(element, true);
            }
        }

        for (int i = 1; i <= length; i++) {
            if (!map.containsKey(i)) {
                return 0;
            }
        }

        return 1;
    }
}
