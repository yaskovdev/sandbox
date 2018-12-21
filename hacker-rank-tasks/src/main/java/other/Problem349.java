package other;

import java.util.HashSet;
import java.util.Set;

class Problem349 {

    int[] intersection(int[] nums1, int[] nums2) {
        final Set<Integer> second = new HashSet<>();
        for (final int n : nums2) {
            second.add(n);
        }
        final Set<Integer> result = new HashSet<>();
        for (final int n : nums1) {
            if (second.contains(n)) {
                result.add(n);
            }
        }
        return result.stream().mapToInt(Number::intValue).toArray();
    }
}
