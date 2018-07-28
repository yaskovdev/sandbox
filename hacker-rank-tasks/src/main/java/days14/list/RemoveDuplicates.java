package days14.list;

/**
 * 2.1 (page 94)
 */
public class RemoveDuplicates {

    public void removeDuplicates(final Node list) {
        Node p1 = list;
        while (p1 != null) {
            Node p2 = p1;
            Node p3 = p1.next;
            while (p3 != null) {
                if (p1.data == p3.data) {
                    p2.next = p3.next;
                    p3 = p2.next;
                } else {
                    p2 = p3;
                    p3 = p3.next;
                }
            }
            p1 = p1.next;
        }
    }
}
