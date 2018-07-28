package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class DeleteMiddleNodeTest {

    @Test
    public void test1() {
        final Node node = new Node(3, new Node(4, new Node(5, null)));
        final Node list = new Node(1, new Node(2, node));
        new DeleteMiddleNode().delete(list, node);
        assertEquals("[1, 2, 4, 5]", new TextOf(list).toString());
    }
}
