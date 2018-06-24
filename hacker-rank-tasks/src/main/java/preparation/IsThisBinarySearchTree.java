package preparation;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class IsThisBinarySearchTree {

    @Test
    public void test0() {
        Node node1 = node(null, null, 1);
        Node node3 = node(null, null, 3);
        Node node5 = node(null, null, 5);
        Node node7 = node(null, null, 7);
        Node node2 = node(node1, node3, 2);
        Node node6 = node(node5, node7, 6);
        Node node4 = node(node2, node6, 4);
        assertTrue(checkBST(node4));
    }

    @Test
    public void test1() {
        Node node2 = node(null, null, 2);
        Node node3 = node(null, null, 3);
        Node node1 = node(node2, node3, 1);
        assertFalse(checkBST(node1));
    }

    @Test
    public void test2() {
        Node node3 = node(null, null, 3);
        Node node1 = node(null, null, 1);
        Node node2 = node(node3, node1, 2);
        assertFalse(checkBST(node2));
    }

    static class Node {
        int data;
        Node left;
        Node right;
    }

    boolean checkBST(final Node root) {
        return checkBST(root, Integer.MIN_VALUE, Integer.MAX_VALUE);
    }

    boolean checkBST(final Node root, int lower, int upper) {
        return root.data > lower && root.data < upper
                && (root.left == null || root.left.data < root.data && checkBST(root.left, lower, Math.min(upper, root.data)))
                && (root.right == null || root.right.data > root.data && checkBST(root.right, Math.max(lower, root.data), upper));
    }

    private static Node node(Node left, Node right, int data) {
        final Node node = new Node();
        node.left = left;
        node.right = right;
        node.data = data;
        return node;
    }
}
