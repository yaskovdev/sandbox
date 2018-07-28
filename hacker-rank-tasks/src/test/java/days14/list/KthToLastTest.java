package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class KthToLastTest {

    @Test
    public void test() {
        final Node list = new Node(1, new Node(3, new Node(4, new Node(1, new Node(2, null)))));
        assertEquals(4, new KthToLast().find(list, 3));
    }
}
