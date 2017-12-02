public class Main {

    public static void main(String[] args) {
        int[][] matrix = create(1, 8);
        print(matrix);
    }

    private static int[][] create(int startValue, int size) {
        if (size == 1) {
            return new int[][]{{startValue}};
        } else {
            final int half = size / 2;
            final int step = half * half;
            return combine(create(startValue, half), create(startValue + step, half),
                    create(startValue + 2 * step, half), create(startValue + 3 * step, half));
        }
    }

    private static int[][] combine(int[][] m1, int[][] m2, int[][] m3, int[][] m4) {
        int initialSize = m1.length;
        int sizeOfResult = initialSize * 2;
        int[][] result = new int[sizeOfResult][sizeOfResult];
        for (int row = 0; row < initialSize; row++) {
            for (int col = 0; col < initialSize; col++) {
                result[row][col] = m1[row][col];
            }
        }
        for (int row = 0; row < initialSize; row++) {
            for (int col = 0; col < initialSize; col++) {
                result[row][col + initialSize] = m2[row][col];
            }
        }
        for (int row = 0; row < initialSize; row++) {
            for (int col = 0; col < initialSize; col++) {
                result[row + initialSize][col] = m3[row][col];
            }
        }
        for (int row = 0; row < initialSize; row++) {
            for (int col = 0; col < initialSize; col++) {
                result[row + initialSize][col + initialSize] = m4[row][col];
            }
        }
        return result;
    }

    private static void print(int[][] matrix) {
        for (int[] row : matrix) {
            for (int val : row) {
                System.out.printf("%3d", val);
            }
            System.out.println();
        }
    }
}
