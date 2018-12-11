package other;

class Problem75 {

    void sortColors(final int[] numbers) {
        int lo = 0;
        int hi = numbers.length - 1;
        int mid = lo;
        while (mid <= hi) {
            final int number = numbers[mid];
            if (number == 0) {
                swap(numbers, lo, mid);
                lo++;
                mid++;
            } else if (number == 1) {
                mid++;
            } else {
                swap(numbers, mid, hi);
                hi--;
            }
        }
    }

    private static void swap(final int[] numbers, final int i, final int j) {
        final int temp = numbers[i];
        numbers[i] = numbers[j];
        numbers[j] = temp;
    }
}
