package atlassian;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Solution {

    public static class LinkedListNode {
        String val;
        LinkedListNode next;

        LinkedListNode(String node_value) {
            val = node_value;
            next = null;
        }
    }

    public static LinkedListNode insert(LinkedListNode head, String val) {
        if (head == null) {
            head = new LinkedListNode(val);
        } else {
            LinkedListNode end = head;
            while (end.next != null) {
                end = end.next;
            }
            LinkedListNode node = new LinkedListNode(val);
            end.next = node;
        }
        return head;
    }

    public static void main(final String[] args) {

    }

    static int find(LinkedListNode list, LinkedListNode subListStart) {
        final List<String> a = new ArrayList<>();
        final List<String> b = new ArrayList<>();

        while (list != null) {
            a.add(list.val);
            list = list.next;
        }
        while (subListStart != null) {
            b.add(subListStart.val);
            subListStart = subListStart.next;
        }
        return Collections.indexOfSubList(a, b);
    }
}
