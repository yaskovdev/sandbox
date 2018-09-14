package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import static java.util.Arrays.asList;

class Problem15 {

    List<List<Integer>> threeSum(final int[] a) {
        final Set<List<Integer>> result = new HashSet<>();
        for (int i = 0; i < a.length; i++) {
            for (int j = i + 1; j < a.length; j++) {
                for (int k = j + 1; k < a.length; k++) {
                    if (a[i] + a[j] + a[k] == 0) {
                        final List<Integer> triple = new ArrayList<>(asList(a[i], a[j], a[k]));
                        Collections.sort(triple);
                        result.add(triple);
                    }
                }
            }
        }
        return new ArrayList<>(result);
    }
}
