package days14.list;

/**
 * 2.3 (page 94)
 */
public class DeleteMiddleNode {

    public void delete(final Node list, final Node node) {
        Node p = list;
        while (p.next != node) {
            p = p.next;
        }
        p.next = node.next;
    }
}
