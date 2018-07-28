package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class SumListsForwardTest {

    @Test
    public void test1() {
        final Node a = new Node(9, new Node(9, new Node(9, null)));
        final Node b = new Node(9, new Node(9, new Node(9, null)));
        assertEquals("[1, 9, 9, 8]", new TextOf(new SumListsForward().sum(a, b)).toString());
    }

    @Test
    public void test2() {
        final Node a = new Node(9, new Node(9, new Node(9, null)));
        final Node b = new Node(9, new Node(9, null));
        assertEquals("[1, 0, 9, 8]", new TextOf(new SumListsForward().sum(a, b)).toString());
    }
}