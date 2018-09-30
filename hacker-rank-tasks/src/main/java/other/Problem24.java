package other;

class Problem24 {

    ListNode swapPairs(ListNode head) {
        if (head == null || head.next == null) {
            return head;
        } else {
            ListNode p = head;
            ListNode q = head.next;
            ListNode newHead = q;

            while (q != null) {
                p.next = q.next;
                q.next = p;
                p = p.next;
                if (p == null) {
                    break;
                }
                q = p.next;
            }

            return newHead;
        }
    }
}
