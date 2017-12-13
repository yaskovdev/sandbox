package codility.training;

import java.util.Arrays;

/**
 * https://codility.com/programmers/lessons/2-arrays/cyclic_rotation/
 */
public class CyclicRotation {

    public static void main(String[] args) {
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{1, 2, 3}, 1)));
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{1, 2, 3}, 2)));
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{1, 2}, 10)));
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{}, 10)));
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{1}, 1000)));
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{3, 8, 9, 7, 6}, 3)));
        System.out.println(Arrays.toString(new CyclicRotation().solution(new int[]{0, 0, 0}, 1)));
    }

    public int[] solution(final int[] array, final int k) {
        if (array.length == 0) {
            return array;
        } else {
            int shift = k % array.length;
            while (shift > 0) {
                shiftToRight(array);
                shift--;
            }
            return array;
        }
    }

    private static void shiftToRight(final int[] array) {
        final int buffer = array[array.length - 1];
        System.arraycopy(array, 0, array, 1, array.length - 1);
        array[0] = buffer;
    }
}
