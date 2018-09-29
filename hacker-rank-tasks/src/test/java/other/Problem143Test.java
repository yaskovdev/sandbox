package other;

import org.junit.Test;

import static other.ListNode.node;

public class Problem143Test {

    @Test
    public void test1() {
        final ListNode head = node(1, node(2, node(3, node(4, null))));
        new Problem143().reorderList(head);
        System.out.println(head);
    }

    @Test
    public void test2() {
        final ListNode head = node(1, node(2, null));
        new Problem143().reorderList(head);
        System.out.println(head);
    }

    @Test
    public void test3() {
        final ListNode head = node(1, node(2, node(3, node(4, node(5, null)))));
        new Problem143().reorderList(head);
        System.out.println(head);
    }
}