package other;

import java.util.HashMap;
import java.util.Map;

public class Problem138 {

    public RandomListNode copyRandomList(RandomListNode head) {
        if (head == null) {
            return null;
        }

        final Map<RandomListNode, RandomListNode> map = new HashMap<>();
        RandomListNode newHead = new RandomListNode(head.label);
        map.put(head, newHead);

        RandomListNode p = head.next;
        RandomListNode newP = newHead;
        while (p != null) {
            newP.next = new RandomListNode(p.label);
            map.put(p, newP.next);
            p = p.next;
            newP = newP.next;
        }

        RandomListNode a = head;
        RandomListNode b = newHead;
        while (a != null) {
            b.random = map.get(a.random);
            a = a.next;
            b = b.next;
        }

        return newHead;
    }
}
