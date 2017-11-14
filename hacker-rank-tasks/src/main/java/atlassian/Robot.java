package atlassian;

import java.util.HashMap;
import java.util.Map;

public class Robot {

    private static Map<Integer, Character> map = map();

    public static void main(final String[] args) {
        System.out.println(solve("PMLPMMMLPMLPMML"));
    }

    private static String solve(final String commands) {
        final RobotState robotState = new RobotState();
        for (final char command : commands.toCharArray()) {
            if (command == 'P') {
                robotState.pickup();
            } else if (command == 'M') {
                robotState.move();
            } else if (command == 'L') {
                robotState.lower();
            }
        }
        return asString(robotState.getPile());
    }

    private static String asString(final int[] result) {
        final StringBuilder builder = new StringBuilder();
        for (final int value : result) {
            builder.append(map.get(value));
        }
        return builder.toString();
    }

    private static Map<Integer, Character> map() {
        final Map<Integer, Character> map = new HashMap<>();
        map.put(0, '0');
        map.put(1, '1');
        map.put(2, '2');
        map.put(3, '3');
        map.put(4, '4');
        map.put(5, '5');
        map.put(6, '6');
        map.put(7, '7');
        map.put(8, '8');
        map.put(9, '9');
        map.put(10, 'A');
        map.put(11, 'B');
        map.put(12, 'C');
        map.put(13, 'D');
        map.put(14, 'E');
        map.put(15, 'F');
        return map;
    }
}
