package codility;

import java.util.HashSet;
import java.util.LinkedList;
import java.util.Queue;
import java.util.Set;

import static codility.Third.Position.p;
import static java.lang.Math.pow;
import static java.util.Arrays.asList;

/**
 * Created by sergei.iaskov on 10/1/2017.
 */
public class Third {

    static class Position {
        int x;
        int y;
        int stepsToThis = 0;
        Position parent;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static Position p(int x, int y, int s, Position parent) {
            final Position position = new Position(x, y);
            position.stepsToThis = s;
            position.parent = parent;
            return position;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            Position position = (Position) o;

            if (x != position.x) return false;
            return y == position.y;
        }

        @Override
        public int hashCode() {
            int result = x;
            result = 31 * result + y;
            return result;
        }
    }

    public static void main(String[] args) {
//        System.out.println(solution(1, 0));
        System.out.println(solution(100_000_000, 100_000_000));
    }

    private static int solution(int a, int b) {
        final Position source = new Position(0, 0);
        final Position dest = new Position(a, b);
        final Set<Position> visited = new HashSet<>();
        final Queue<Position> positionsToHandle = new LinkedList<>();
        positionsToHandle.add(source);
        while (!positionsToHandle.isEmpty()) {
            final Position current = positionsToHandle.remove();
            if (current.stepsToThis > 100_000_000) {
                return -2;
            }
            if (!visited.contains(current)) {
                System.out.println(current.x + " " + current.y);
                if (current.x == dest.x && current.y == dest.y) {
                    return current.stepsToThis;
                } else {
                    visited.add(current);
                    final Set<Position> reachableFromCurrent = reachableFrom(current);
                    for (Position position : reachableFromCurrent) {
                        if (closerThanItsGrandparent(position, dest)) {
                            positionsToHandle.add(position);
                        }
                    }
                }
            }
        }
        return -1;
    }

    private static Set<Position> reachableFrom(final Position p) {
        final int s = p.stepsToThis + 1;
        final Position g = p.parent;
        return new HashSet<>(asList(p(p.x + 1, p.y + 2, s, p), p(p.x + 2, p.y + 1, s, p),
                p(p.x + 2, p.y - 1, s, p), p(p.x + 1, p.y - 2, s, p), p(p.x - 1, p.y - 2, s, p),
                p(p.x - 2, p.y - 1, s, p), p(p.x - 2, p.y + 1, s, p), p(p.x - 1, p.y + 2, s, p)));
    }

    private static boolean closerThanItsGrandparent(Position position, Position dest) {
        Position grandparent = position.parent.parent;
        if (grandparent == null) {
            return true;
        } else {
            Position grandgrandparent = grandparent.parent;
            return grandgrandparent == null || dist(position, dest) <= dist(grandgrandparent, dest);
        }
    }

    private static int dist(Position position, Position dest) {
        return (int) (pow(dest.x - position.x, 2) + pow(dest.y - position.y, 2));
    }
}
