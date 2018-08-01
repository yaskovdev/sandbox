package days14.stack;

import java.util.TreeMap;

import static java.util.Objects.isNull;

/**
 * 3.2
 */
public class StackMin {

    private final int[] elements = new int[100];
    private final TreeMap<Integer, Integer> map = new TreeMap<>();
    private int index = 0;

    public void push(int value) {
        elements[index] = value;
        index++;
        final Integer frequency = map.get(value);
        map.put(value, isNull(frequency) ? 1 : frequency + 1);
    }

    public int pop() {
        index--;
        final int element = elements[index];
        final Integer frequency = map.get(element);
        if (frequency == 1) {
            map.remove(element);
        } else {
            map.put(element, frequency - 1);
        }
        return element;
    }

    public int peek() {
        return elements[index - 1];
    }

    public int min() {
        return map.firstKey();
    }
}
