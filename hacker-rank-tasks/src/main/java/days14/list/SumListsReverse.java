package days14.list;

/**
 * 2.5 (page 95)
 */
public class SumListsReverse {

    Node sum(final Node a, final Node b) {
        Node p1 = a;
        Node p2 = b;
        Node p3Start = null;
        Node p3End = null;
        int addition = 0;
        while (p1 != null || p2 != null) {
            final int sum = dataOf(p1) + dataOf(p2) + addition;
            final int value = sum > 9 ? sum - 10 : sum;
            addition = sum > 9 ? 1 : 0;
            Node node = new Node(value, null);
            if (p3Start == null) {
                p3Start = node;
                p3End = node;
            } else {
                p3End.next = node;
                p3End = node;
            }
            if (p1 != null) {
                p1 = p1.next;
            }
            if (p2 != null) {
                p2 = p2.next;
            }
        }
        if (addition > 0) {
            p3End.next = new Node(1, null);
        }
        return p3Start;
    }

    private static int dataOf(Node node) {
        return node == null ? 0 : node.data;
    }
}
