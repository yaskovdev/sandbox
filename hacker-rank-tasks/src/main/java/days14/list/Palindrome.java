package days14.list;

/**
 * 2.6 (page 95)
 */
public class Palindrome {

    public boolean isPalindrome(final Node list) {
        Node p = list;
        int n = 1;
        while (p.next != null) {
            p = p.next;
            n++;
        }

        Node middle = list;
        for (int i = 0; i < n / 2; i++) {
            middle = middle.next;
        }

        Node next = middle.next;
        while (next != null) {
            Node temp = next.next;
            next.next = middle;
            middle = next;
            next = temp;
        }

        Node start = list;
        for (int i = 0; i < n / 2; i++) {
            if (start.data != middle.data) {
                return false;
            } else {
                start = start.next;
                middle = middle.next;
            }
        }

        return true;
    }
}
