package other;

import org.junit.Test;

import static other.ListNode.node;

public class Problem19Test {

    @Test
    public void test1() {
        final ListNode result = new Problem19().removeNthFromEnd(node(1, node(2, node(3, node(4, node(5, null))))), 2);
        System.out.println(result);
    }
}
