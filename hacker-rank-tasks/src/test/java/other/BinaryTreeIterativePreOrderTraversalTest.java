package other;

import org.junit.Test;

import java.util.List;

public class BinaryTreeIterativePreOrderTraversalTest {

    @Test
    public void preOrderTraversal() {
        final List<Integer> nodes = new BinaryTreeIterativePreOrderTraversal().preOrderTraversal(node(0, node(1), node(2, node(3), node(4))));
        System.out.println(nodes);
    }

    private static TreeNode node(int value, TreeNode left, TreeNode right) {
        final TreeNode node = new TreeNode(value);
        node.left = left;
        node.right = right;
        return node;
    }

    private static TreeNode node(int value) {
        return node(value, null, null);
    }
}
