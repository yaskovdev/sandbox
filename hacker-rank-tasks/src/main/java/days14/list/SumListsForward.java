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

        Node p3Start = null;
        Node p3End = null;
        Node p3PreEnd = null;

        while (p1 != null) {
            int sum = p1.data + p2.data;
            int value = sum > 9 ? sum - 10 : sum;
            Node node = new Node(value, null);
            if (p3Start == null) {
                p3Start = node;
                p3End = node;
            } else {
                p3End.next = node;
                p3PreEnd = p3End;
                p3End = node;
            }
            if (sum > 9) {
                if (p3PreEnd == null) {
                    p3PreEnd = new Node(1, p3Start);
                    p3Start = p3PreEnd;
                } else {
                    p3PreEnd.data += 1;
                }
            }
            p1 = p1.next;
            p2 = p2.next;
        }
        return p3Start;
    }
}
