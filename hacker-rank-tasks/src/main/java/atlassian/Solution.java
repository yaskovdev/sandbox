package atlassian;

import java.util.Arrays;

public class Solution {

    private static class LinkedListNode {
        String val;
        LinkedListNode next;

        private LinkedListNode(String nodeValue) {
            val = nodeValue;
            next = null;
        }
    }

    private static LinkedListNode insert(LinkedListNode head, String val) {
        if (head == null) {
            head = new LinkedListNode(val);
        } else {
            LinkedListNode end = head;
            while (end.next != null) {
                end = end.next;
            }
            end.next = new LinkedListNode(val);
        }
        return head;
    }

    private static LinkedListNode list(final String... values) {
        final LinkedListNode list = insert(null, values[0]);
        LinkedListNode head = list;
        for (final String value : Arrays.copyOfRange(values, 1, values.length)) {
            head = insert(list, value);
        }
        return head;
    }

    public static void main(final String[] args) {
        System.out.println(find(list("1", "2", "3"), list("2", "3")));
        System.out.println(find(list("1", "2", "3"), list("3", "2")));
        System.out.println(find(list("1", "2", "3"), list("1", "2")));
        System.out.println(find(list("1", "3", "2", "1", "2"), list("3", "2", "1")));
    }

    static void print(final LinkedListNode list) {
        LinkedListNode node = list;
        while (node != null) {
            System.out.print(node.val + " ");
            node = node.next;
        }
    }

    private static int find(LinkedListNode list, LinkedListNode subListStart) {
        LinkedListNode i = list;
        LinkedListNode j = subListStart;
        int count = 0;
        while (i != null && j != null) {
            if (i.val.equals(j.val)) {
                i = i.next;
                j = j.next;
            } else if (j == subListStart) {
                i = i.next;
                count++;
            } else {
                j = subListStart;
            }
        }
        return j == null ? count : -1;
    }
}
