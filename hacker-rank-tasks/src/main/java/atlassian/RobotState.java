package atlassian;

import static java.util.Arrays.fill;

/**
 * Created by Sergey on 14.11.2017.
 */
public class RobotState {

    private final int[] pile;
    private int position = -1;
    private boolean holdsBlock = false;

    public RobotState() {
        pile = new int[10];
        fill(pile, 0);
    }

    public void pickup() {
        if (!holdsBlock) {
            holdsBlock = true;
        }
        position = 0;
    }

    public void move() {
        if (position < 9) {
            position++;
        }
    }

    public void lower() {
        if (holdsBlock) {
            if (pile[position] < 15) {
                pile[position]++;
                holdsBlock = false;
            }
        }
    }

    public int[] getPile() {
        return pile;
    }
}
