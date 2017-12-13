package codility.training;

import java.util.Arrays;

/**
 * https://codility.com/programmers/lessons/2-arrays/odd_occurrences_in_array/
 */
public class OddOccurrencesInArray {

    public static void main(String[] args) {
        System.out.println(new OddOccurrencesInArray().solution(new int[]{9, 3, 9, 3, 9, 7, 9}));
        System.out.println(new OddOccurrencesInArray().solution(new int[]{1, 1, 1, 2, 2}));
        System.out.println(new OddOccurrencesInArray().solution(new int[]{42}));
    }

    /**
     * That is not the best solution. The best is O(n) and it is using XOR.
     */
    public int solution(final int[] array) {
        Arrays.sort(array);

        for (int i = 0; i <= array.length - 2; i += 2) {
            if (array[i] != array[i + 1]) {
                return array[i];
            }
        }
        return array[array.length - 1];
    }
}
