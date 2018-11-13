package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertSame;

public class IntersectionTest {

    @Test
    public void test1() {
        final Node node = new Node(3, new Node(7, null));
        final Node list1 = new Node(1, new Node(2, node));
        final Node list2 = new Node(4, new Node(5, new Node(6, node)));
        assertSame(node, new Intersection().intersection(list1, list2));
    }

    @Test
    public void test2() {
        final Node list1 = new Node(1, new Node(6, null));
        final Node list2 = new Node(4, new Node(5, new Node(6, null)));
        assertNull(new Intersection().intersection(list1, list2));
    }
}
