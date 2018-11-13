package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class PalindromeTest {

    @Test
    public void test1() {
        final Node list = new Node(1, new Node(3, new Node(2, new Node(2, new Node(3, new Node(1, null))))));
        assertTrue(new Palindrome().isPalindrome(list));
    }

    @Test
    public void test2() {
        final Node list = new Node(1, new Node(3, new Node(2, new Node(3, new Node(1, null)))));
        assertTrue(new Palindrome().isPalindrome(list));
    }

    @Test
    public void test3() {
        final Node list = new Node(1, null);
        assertTrue(new Palindrome().isPalindrome(list));
    }

    @Test
    public void test4() {
        final Node list = new Node(1, new Node(2, null));
        assertFalse(new Palindrome().isPalindrome(list));
    }

    @Test
    public void test5() {
        final Node list = new Node(1, new Node(2, new Node(3, null)));
        assertFalse(new Palindrome().isPalindrome(list));
    }
}
