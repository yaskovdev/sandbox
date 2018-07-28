package days14.list;

/**
 * 2.2 (page 94)
 */
public class KthToLast {

    public int find(final Node list, final int k) {
        Node p1 = list;
        Node p2 = p1;
        for (int i = 0; i < k - 1; i++) {
            p2 = p2.next;
        }

        while (p2.next != null) {
            p1 = p1.next;
            p2 = p2.next;
        }
        return p1.data;
    }
}
