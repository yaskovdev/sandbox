package other;

public class Problem2 {

	public ListNode addTwoNumbers(ListNode l1, ListNode l2) {
		ListNode p1 = l1;
		ListNode p2 = l2;
		ListNode p3 = null;
		ListNode result = null;

		int acc = 0;
		while (p1 != null || p2 != null) {
			final int a = p1 == null ? 0 : p1.val;
			final int b = p2 == null ? 0 : p2.val;

			final int sum = a + b + acc;

			ListNode node;
			if (sum >= 10) {
				acc = 1;
				node = new ListNode(sum - 10);
			} else {
				acc = 0;
				node = new ListNode(sum);
			}

			if (p3 == null) {
				p3 = node;
				result = node;
			} else {
				p3.next = node;
				p3 = node;
			}

			p1 = p1 == null ? null : p1.next;
			p2 = p2 == null ? null : p2.next;
		}

		if (acc > 0) {
			p3.next = new ListNode(acc);
		}

		return result;
	}
}
