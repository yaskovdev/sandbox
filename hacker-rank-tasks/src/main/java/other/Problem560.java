package other;

class Problem560 {

    int subarraySum(int[] a, int k) {
        int count = 0;
        for (int i = 0; i < a.length; i++) {
            int sum = a[i];
            for (int j = i; j < a.length; j++) {
                if (i != j) {
                    sum = sum + a[j];
                }
                if (sum == k) {
                    count++;
                }
            }
        }
        return count;
    }
}
