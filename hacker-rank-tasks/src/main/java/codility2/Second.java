package codility2;

import java.util.HashMap;
import java.util.Map;

public class Second {

    public static void main(final String[] args) {
        System.out.println(new Second().solution(new int[]{6, 10, 6, 9, 7, 8}));
        System.out.println(new Second().solution(new int[]{6}));
    }

    public int solution(final int[] array) {
        final Map<Integer, Integer> map = elementToItsFrequency(array);
        return twoNeighboursWithMaxFrequencySum(map);
    }

    private static Map<Integer, Integer> elementToItsFrequency(final int[] array) {
        final Map<Integer, Integer> result = new HashMap<>();
        for (final int element : array) {
            result.compute(element, (key, value) -> value == null ? 1 : value + 1);
        }
        return result;
    }

    private int twoNeighboursWithMaxFrequencySum(final Map<Integer, Integer> map) {
        int max = 0;
        for (final Map.Entry<Integer, Integer> entry : map.entrySet()) {
            int candidate = entry.getValue() + map.getOrDefault(entry.getKey() + 1, 0);
            if (candidate > max) {
                max = candidate;
            }
        }
        return max;
    }
}
