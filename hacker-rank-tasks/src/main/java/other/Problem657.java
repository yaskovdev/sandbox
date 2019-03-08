package other;

class Problem657 {

    boolean judgeCircle(String moves) {
        return judge(moves, 0, 0);
    }

    private static boolean judge(final String moves, final int x, final int y) {
        if (moves.isEmpty()) {
            return x == 0 && y == 0;
        } else {
            final char move = moves.toCharArray()[0];
            final String rest = moves.substring(1);
            if (move == 'R') {
                return judge(rest, x + 1, y);
            } else if (move == 'L') {
                return judge(rest, x - 1, y);
            } else if (move == 'U') {
                return judge(rest, x, y + 1);
            } else if (move == 'D') {
                return judge(rest, x, y - 1);
            } else {
                throw new RuntimeException("unexpected move " + move);
            }
        }
    }
}
