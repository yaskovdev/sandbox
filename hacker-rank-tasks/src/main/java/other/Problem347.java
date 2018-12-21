package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

class Problem347 {

    static class Pair implements Comparable<Pair> {
        final int number;
        final int frequency;

        Pair(final int number, final int frequency) {
            this.number = number;
            this.frequency = frequency;
        }

        @Override
        public int compareTo(final Pair other) {
            return other.frequency - this.frequency;
        }
    }

    List<Integer> topKFrequent(final int[] numbers, final int k) {
        final Map<Integer, Integer> numberToFrequency = new HashMap<>();
        for (final int number : numbers) {
            final Integer frequency = numberToFrequency.computeIfAbsent(number, key -> 0);
            numberToFrequency.put(number, frequency + 1);
        }

        final List<Pair> pairs = new ArrayList<>();

        for (Entry<Integer, Integer> entry : numberToFrequency.entrySet()) {
            pairs.add(new Pair(entry.getKey(), entry.getValue()));
        }

        Collections.sort(pairs);

        final List<Integer> output = new ArrayList<>(k);
        for (int i = 0; i < k; i++) {
            output.add(pairs.get(i).number);
        }

        return output;
    }
}
