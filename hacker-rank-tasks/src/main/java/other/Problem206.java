package other;

public class Problem206 {

    public ListNode reverseList(final ListNode head) {
        return reverseList(null, head);
    }

    private ListNode reverseList(final ListNode p, final ListNode ps) {
        if (ps == null) {
            return p;
        } else {
            final ListNode newPs = ps.next;
            ps.next = p;
            return reverseList(ps, newPs);
        }
    }
}
