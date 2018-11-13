package other;

import java.util.Arrays;

class Problem31 {

    void nextPermutation(final int[] numbers) {
        for (int i = 1; i < numbers.length; i++) {
            for (int j = numbers.length - i - 1; j >= 0; j--) {
                if (numbers[j] < numbers[j + 1]) {
                    swap(numbers, j, j + 1);
                    return;
                }
            }
        }
        Arrays.sort(numbers);
    }

    private void swap(final int[] numbers, final int i, final int j) {
        int temp = numbers[i];
        numbers[i] = numbers[j];
        numbers[j] = temp;
    }
}
