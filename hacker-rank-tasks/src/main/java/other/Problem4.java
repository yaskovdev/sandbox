package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

class Problem4 {

    double findMedianSortedArrays(int[] nums1, int[] nums2) {
        List<Integer> list = new ArrayList<>(nums1.length + nums2.length);
        for (int num : nums1) {
            list.add(num);
        }
        for (int num : nums2) {
            list.add(num);
        }
        Collections.sort(list);
        if (list.size() % 2 == 0) {
            return ((double) list.get(list.size() / 2 - 1) + list.get(list.size() / 2)) / 2;
        } else {
            return list.get(list.size() / 2);
        }
    }
}
