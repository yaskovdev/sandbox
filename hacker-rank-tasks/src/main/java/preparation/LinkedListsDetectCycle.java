package preparation;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class LinkedListsDetectCycle {

    @Test
    public void test0() {
        final Node third = node(3, null);
        final Node second = node(2, third);
        final Node list = node(1, second);
        third.next = second;
        assertEquals(true, hasCycle(list));
    }

    @Test
    public void test1() {
        final Node list = node(1, null);
        assertEquals(false, hasCycle(list));
    }

    private static Node node(int data, Node next) {
        Node node = new Node();
        node.data = data;
        node.next = next;
        return node;
    }

    static class Node {
        int data;
        Node next;
    }

    boolean hasCycle(Node head) {
        Node slowPointer = head;
        Node fastPointer = head;
        while (slowPointer != null && fastPointer != null && fastPointer.next != null) {
            slowPointer = slowPointer.next;
            fastPointer = fastPointer.next.next;
            if (slowPointer == fastPointer) {
                return true;
            }
        }
        return false;
    }
}
