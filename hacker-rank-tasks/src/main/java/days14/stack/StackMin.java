package days14.stack;

/**
 * 3.2
 */
public class StackMin<T> {

    private final int[] elements = new int[100];
    private int index = 0;
    private int min = Integer.MAX_VALUE;

    public void push(int value) {
        elements[index] = value;
        index++;
        min = Math.min(min, value);
    }

    public int pop() {
        index--;
        return elements[index];
    }

    public int peek() {
        return elements[index - 1];
    }

    public int min() {
        return min;
    }
}
