package other;

class Problem143 {

    void reorderList(final ListNode head) {
        final int size = size(head);
        if (head != null && head.next != null) { // if this is empty list or singleton, we are done
            ListNode p = head;
            ListNode q = head.next;
            int i = 0;
            while (i < size / 2) {
                p = q;
                q = q.next;
                i++;
            }
            p.next = null;
            while (q != null) {
                ListNode temp = q.next;
                q.next = p;
                p = q;
                q = temp;
            }
            q = p;
            p = head;

            while (true) {
                ListNode temp1 = p.next;
                if (temp1 == q) {
                    break;
                }
                p.next = q;
                ListNode temp2 = q.next;
                q.next = temp1;
                p = temp1;
                q = temp2;
                if (p == q) {
                    break;
                }
            }
        }
    }

    private static int size(final ListNode head) {
        return head == null ? 0 : 1 + size(head.next);
    }
}
