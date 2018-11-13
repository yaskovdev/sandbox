package days14.list;

/**
 * 2.8 (page 95)
 */
public class LoopDetection {

    Node detectLoop(final Node list) {
        if (list.next == null) {
            return null;
        }
        Node slow = list;
        Node fast = list.next;
        while (slow != null && fast.next != null) {
            if (slow == fast) {
                return slow;
            }
            slow = slow.next;
            fast = fast.next.next;
        }
        return null;
    }
}
