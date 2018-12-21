package other;

class Problem19 {

    ListNode removeNthFromEnd(final ListNode head, final int n) {
        ListNode start = head;
        ListNode end = head;

        for (int i = 0; i < n; i++) {
            end = end.next;
        }

        if (end == null) {
            return head.next;
        } else {
            while (end.next != null) {
                start = start.next;
                end = end.next;
            }

            start.next = start.next.next;
            return head;
        }
    }
}
