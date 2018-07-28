package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class RemoveDuplicatesTest {

    @Test
    public void test1() {
        final Node list = new Node(1, new Node(3, new Node(4, new Node(1, new Node(2, null)))));
        new RemoveDuplicates().removeDuplicates(list);
        assertEquals("[1, 3, 4, 2]", new TextOf(list).toString());
    }

    @Test
    public void test2() {
        final Node list = new Node(1, new Node(1, new Node(1, new Node(1, new Node(1, null)))));
        new RemoveDuplicates().removeDuplicates(list);
        assertEquals("[1]", new TextOf(list).toString());
    }

    @Test
    public void test3() {
        final Node list = new Node(1, new Node(2, new Node(2, new Node(1, new Node(3, null)))));
        new RemoveDuplicates().removeDuplicates(list);
        assertEquals("[1, 2, 3]", new TextOf(list).toString());
    }
}
