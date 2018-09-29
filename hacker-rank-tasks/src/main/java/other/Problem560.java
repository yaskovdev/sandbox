package other;

class Problem560 {

    int subarraySum(int[] a, int k) {
        int count = 0;
        for (int i = 0; i < a.length; i++) {
            for (int j = i; j < a.length; j++) {
                if (sum(a, i, j) == k) {
                    count++;
                }
            }
        }
        return count;
    }

    private static int sum(int[] a, int i, int j) {
        int sum = 0;
        for (int k = i; k <= j; k++) {
            sum += a[k];
        }
        return sum;
    }
}
