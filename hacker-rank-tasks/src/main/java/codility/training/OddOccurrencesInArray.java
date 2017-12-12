package codility.training;

/**
 * https://codility.com/programmers/lessons/2-arrays/odd_occurrences_in_array/
 */
public class OddOccurrencesInArray {

    public static void main(String[] args) {
        System.out.println(new OddOccurrencesInArray().solution(new int[]{9, 3, 9, 3, 9, 7, 9}));
        System.out.println(new OddOccurrencesInArray().solution(new int[]{1, 1, 1, 2, 2}));
    }

    public int solution(final int[] array) {
        for (final int i : array) {
            int occurred = 0;
            for (final int j : array) {
                if (j == i) {
                    occurred++;
                }
            }
            if (occurred % 2 == 1) {
                return i;
            }
        }
        throw new RuntimeException("cannot get here");
    }
}
