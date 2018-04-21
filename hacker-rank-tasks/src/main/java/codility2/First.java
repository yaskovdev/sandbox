package codility2;

import java.util.HashSet;
import java.util.Set;

public class First {

    private static class Point2D {
        public int x;
        public int y;
    }

    private static Point2D p(int x, int y) {
        Point2D point = new Point2D();
        point.x = x;
        point.y = y;
        return point;
    }

    public static void main(final String[] args) {
        System.out.println(new First().solution(new Point2D[]{p(-1, -2), p(1, 2), p(2, 4), p(-3, 2), p(2, -2)}));
        System.out.println(new First().solution(new Point2D[]{p(-1_000_000_000, 1_000_000_000), p(1_000_000_000, -1_000_000_000)}));
        System.out.println(new First().solution(new Point2D[]{p(-1, 0), p(-2, 0)}));
        System.out.println(new First().solution(new Point2D[]{p(-1, 0), p(1, 0), p(0, 1), p(0, 2)}));
    }

    public int solution(final Point2D[] points) {
        final Set<String> fractions = new HashSet<>();
        for (final Point2D point : points) {
            final String fraction = asFraction(point.x, point.y);
            fractions.add(fraction);
        }
        return fractions.size();
    }

    private static String asFraction(final int a, final int b) {
        final long gcm = Math.abs(gcm(a, b));
        return (a / gcm) + "/" + (b / gcm);
    }

    private static long gcm(final int a, final int b) {
        return b == 0 ? a : gcm(b, a % b);
    }
}
