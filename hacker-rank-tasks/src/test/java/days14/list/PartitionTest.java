package days14.list;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class PartitionTest {

    @Test
    public void partition() {
        final Node list = new Node(3, new Node(5, new Node(8, new Node(5, new Node(10, new Node(2, new Node(1, null)))))));
        assertEquals("[3, 2, 1, 5, 8, 5, 10]", new TextOf(new Partition().partition(list, 5)).toString());
    }
}
