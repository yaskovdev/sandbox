package days14.list;

/**
 * 2.7 (page 95)
 */
public class Intersection {

    Node intersection(final Node l1, final Node l2) {
        Node p1 = l1;
        while (p1 != null) {
            Node p2 = l2;
            while (p2 != null) {
                if (p1 == p2) {
                    return p1;
                }
                p2 = p2.next;
            }
            p1 = p1.next;
        }
        return null;
    }
}
