package other;

class ListNode {
    int val;
    ListNode next;

    ListNode(int x) {
        val = x;
    }

    public static ListNode node(int value, ListNode next) {
        final ListNode node = new ListNode(value);
        node.next = next;
        return node;
    }
}
