package days14.stack;

/**
 * 3.2
 */
public class StackMin {

    private final int[] elements = new int[100];
    private int index = 0;

    public void push(int value) {
        elements[index] = value;
        index++;
    }

    public int pop() {
        index--;
        return elements[index];
    }

    public int peek() {
        return elements[index - 1];
    }

    public int min() {
        int candidate = Integer.MAX_VALUE;
        for (int i = 0; i < index; i++) {
            candidate = Math.min(candidate, elements[i]);
        }
        return candidate;
    }
}
