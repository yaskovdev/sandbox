package days14.list;

/**
 * 2.3 (page 94)
 */
public class DeleteMiddleNode {

    public void delete(final Node node) {
        node.data = node.next.data;
        node.next = node.next.next;
    }
}
