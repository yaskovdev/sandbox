package other;

class Problem24 {

    ListNode swapPairs(ListNode head) {
        if (head == null || head.next == null) {
            return head;
        } else {
            ListNode p = head;
            ListNode q = head.next;

            if (q.next == null) {
                p.next = null;
                q.next = p;
                return q;
            }

            ListNode newHead = q;
            while (q != null && q.next != null) {
                p.next = q.next.next == null ? q.next : q.next.next;
                ListNode t = q.next;
                q.next = p;
                p = t;
                q = p.next;
            }

            if (q != null) {
                p.next = null;
                q.next = p;
            }

            return newHead;
        }
    }
}
