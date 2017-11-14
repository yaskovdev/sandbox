package atlassian;

import static java.util.Arrays.fill;

class RobotState {

    private final int[] pile;
    private int position = -1;
    private boolean holdsBlock = false;

    RobotState() {
        pile = new int[10];
        fill(pile, 0);
    }

    void pickup() {
        holdsBlock = true;
        position = 0;
    }

    void move() {
        if (position < 9) {
            position++;
        }
    }

    void lower() {
        if (holdsBlock && pile[position] < 15) {
            pile[position]++;
            holdsBlock = false;
        }
    }

    int[] getPile() {
        return pile;
    }
}
