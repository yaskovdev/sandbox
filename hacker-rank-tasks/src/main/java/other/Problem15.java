package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

import static java.util.Arrays.asList;

class Problem15 {

    List<List<Integer>> threeSum(final int[] a) {
        final Set<List<Integer>> result = new HashSet<>();
        final Map<Integer, Integer> numberToFrequency = new HashMap<>();
        for (int number : a) {
            final Integer frequency = numberToFrequency.computeIfAbsent(number, k -> 0);
            numberToFrequency.put(number, frequency + 1);
        }
        for (int i = 0; i < a.length; i++) {
            for (int j = i + 1; j < a.length; j++) {
                final int sum = a[i] + a[j];
                final int requiredFrequency = 1 + (a[i] == -sum ? 1 : 0) + (a[j] == -sum ? 1 : 0);
                if (numberToFrequency.getOrDefault(-sum, 0) >= requiredFrequency) {
                    final List<Integer> triple = new ArrayList<>(asList(a[i], a[j], -sum));
                    Collections.sort(triple);
                    result.add(triple);
                }
            }
        }
        return new ArrayList<>(result);
    }
}
