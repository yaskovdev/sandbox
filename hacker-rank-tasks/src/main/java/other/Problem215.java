package other;

import java.util.Arrays;

class Problem215 {

    int findKthLargest(int[] numbers, int k) {
        Arrays.sort(numbers);
        return numbers[numbers.length - k];
    }
}
