package days14.string;

/**
 * 1.8 (page 91)
 */
public class ZeroMatrix {

    // TODO: is it a good idea to use the nullable type?
    public void zero(final Integer[][] matrix) {
        int m = matrix.length;
        int n = matrix[0].length;
        for (int x = 0; x < m; x++) {
            for (int y = 0; y < n; y++) {
                if (matrix[x][y] == 0) {
                    matrix[x][y] = null;
                }
            }
        }

        for (int x = 0; x < m; x++) {
            for (int y = 0; y < n; y++) {
                if (matrix[x][y] == null) {
                    for (int i = 0; i < m; i++) {
                        if (matrix[i][y] != null) {
                            matrix[i][y] = 0;
                        }
                    }
                    for (int j = 0; j < n; j++) {
                        if (matrix[x][j] != null) {
                            matrix[x][j] = 0;
                        }
                    }
                    matrix[x][y] = 0;
                }
            }
        }
    }
}
