package other;

class SpiralArray {

    int[][] spiral(final int n) {
        int[][] a = new int[n][n];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                a[i][j] = 0;
            }
        }

        int c = 1;

        int i = 0;
        int j = 0;
        while (c < n * n) {
            while (i < n - 1 && a[i + 1][j] == 0) {
                a[i][j] = c;
                i++;
                c++;
            }
            while (j < n - 1 && a[i][j + 1] == 0) {
                a[i][j] = c;
                j++;
                c++;
            }
            while (i > 0 && a[i - 1][j] == 0) {
                a[i][j] = c;
                i--;
                c++;
            }
            while (j > 0 && a[i][j - 1] == 0) {
                a[i][j] = c;
                j--;
                c++;
            }
        }
        a[i][j] = c;

        return a;
    }
}
