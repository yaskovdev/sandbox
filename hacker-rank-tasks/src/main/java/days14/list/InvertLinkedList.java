package days14.list;

import org.junit.Test;

import static java.util.Objects.isNull;
import static org.junit.Assert.assertEquals;

/**
 * Note: the most tricky part here is to not forget to point the next node of the initial first node to null.
 * Otherwise there will be a loop in the list.
 */
public class InvertLinkedList {

    @Test
    public void test1() {
        final Node list = new Node(3, new Node(7, new Node(1, new Node(2, null))));
        assertEquals("[3, 7, 1, 2]", toString(list));
    }

    @Test
    public void test2() {
        final Node list = new Node(3, new Node(7, new Node(1, new Node(2, null))));
        assertEquals("[2, 1, 7, 3]", toString(invert(list)));
    }

    @Test
    public void test3() {
        final Node list = new Node(3, null);
        assertEquals("[3]", toString(invert(list)));
    }

    private Node invert(final Node list) {
        Node a = list;
        Node b = list.next;
        while (!isNull(b)) {
            final Node temp = b.next;
            b.next = a;
            a = b;
            b = temp;
        }
        list.next = null;
        return a;
    }

    private String toString(Node list) {
        final StringBuilder builder = new StringBuilder("[");
        while (!isNull(list)) {
            builder.append(list.data).append(isNull(list.next) ? "" : ", ");
            list = list.next;
        }
        return builder.append("]").toString();
    }
}
