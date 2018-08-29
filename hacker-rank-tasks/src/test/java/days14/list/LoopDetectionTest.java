package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertSame;

public class LoopDetectionTest {

    @Test
    public void detectLoop() {
        final Node node1 = new Node(1, null);
        final Node node3 = new Node(3, new Node(4, node1));
        final Node list = new Node(1, node3);
        node1.next = node3;
        assertSame(node3, new LoopDetection().detectLoop(list));
    }
}