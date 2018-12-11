package other;

import org.junit.Test;

import static other.ListNode.node;

public class Problem24Test {

    @Test
    public void test1() {
        final ListNode head = new Problem24().swapPairs(node(1, node(2, node(3, node(4, null)))));
        System.out.println(head);
    }

    @Test
    public void test2() {
        final ListNode head = new Problem24().swapPairs(node(1, node(2, node(3, null))));
        System.out.println(head);
    }

    @Test
    public void test3() {
        final ListNode head = new Problem24().swapPairs(node(1, node(2, null)));
        System.out.println(head);
    }
}
