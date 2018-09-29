package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static other.TreeNode.node;

public class Problem543Test {

    @Test
    public void test0() {
        assertEquals(2, new Problem543().diameterOfBinaryTree(node(2, node(1), node(3))));
    }

    @Test
    public void test1() {
        assertEquals(1, new Problem543().diameterOfBinaryTree(node(2, node(1), null)));
    }
}
