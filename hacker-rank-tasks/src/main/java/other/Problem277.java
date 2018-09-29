package other;

class Problem277 extends Relation {

    public int findCelebrity(int n) {
        final boolean[][] matrix = new boolean[n][n];
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix.length; j++) {
                matrix[i][j] = knows(i, j);
            }
        }

        for (int i = 0; i < matrix.length; i++) {
            final boolean[] person = matrix[i];
            if (knowsNobody(person) && everyoneKnows(matrix, i)) {
                return i;
            }
        }

        return -1;
    }

    private boolean everyoneKnows(boolean[][] knows, int j) {
        for (int i = 0; i < knows.length; i++) {
            if (!knows[i][j] && i != j) {
                return false;
            }
        }
        return true;
    }

    private boolean knowsNobody(boolean[] person) {
        for (boolean b : person) {
            if (b) {
                return false;
            }
        }
        return true;
    }
}
