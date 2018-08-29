package days14.list;

public class SumListsForward {

    Node sum(final Node a, final Node b) {
        Node p1 = a;
        Node p2 = b;
        int aLength = 0;
        int bLength = 0;
        while (p1 != null) {
            aLength++;
            p1 = p1.next;
        }
        while (p2 != null) {
            bLength++;
            p2 = p2.next;
        }
        if (aLength > bLength) {
            p1 = a;
            p2 = b;
            for (int i = 0; i < aLength - bLength; i++) {
                p2 = new Node(0, p2);
            }
        } else if (bLength > aLength) {
            p1 = b;
            p2 = a;
            for (int i = 0; i < bLength - aLength; i++) {
                p2 = new Node(0, p2);
            }
        } else {
            p1 = a;
            p2 = b;
        }

        Node p3 = null;

        while (p1 != null) {
            final int sum = p1.data + p2.data;
            p3 = new Node(sum, p3);
            p1 = p1.next;
            p2 = p2.next;
        }

        Node p4 = p3;
        int addition = 0;
        while (p4.next != null) {
            int sum = p4.data + addition;
            p4.data = sum > 9 ? sum - 10 : sum;
            addition = sum > 9 ? 1 : 0;
            p4 = p4.next;
        }
        if (addition > 0) {
            p4.data -= 9;
            p4.next = new Node(1, null);
        }
        return p3;
    }
}
