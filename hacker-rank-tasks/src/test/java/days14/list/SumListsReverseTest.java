package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class SumListsReverseTest {

    @Test
    public void test1() {
        final Node a = new Node(7, new Node(1, new Node(6, null)));
        final Node b = new Node(5, new Node(9, new Node(2, null)));
        assertEquals("[2, 1, 9]", new TextOf(new SumListsReverse().sum(a, b)).toString());
    }

    @Test
    public void test2() {
        final Node a = new Node(9, new Node(9, new Node(9, null)));
        final Node b = new Node(9, new Node(9, new Node(9, null)));
        assertEquals("[8, 9, 9, 1]", new TextOf(new SumListsReverse().sum(a, b)).toString());
    }

    @Test
    public void test3() {
        final Node a = new Node(1, null);
        final Node b = new Node(9, new Node(9, new Node(9, null)));
        assertEquals("[0, 0, 0, 1]", new TextOf(new SumListsReverse().sum(a, b)).toString());
    }

    @Test
    public void test4() {
        final Node a = new Node(9, new Node(7, new Node(8, null)));
        final Node b = new Node(6, new Node(8, new Node(5, null)));
        assertEquals("[5, 6, 4, 1]", new TextOf(new SumListsReverse().sum(a, b)).toString());
    }
}
