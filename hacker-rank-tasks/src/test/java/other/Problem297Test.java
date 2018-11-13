package other;

import org.junit.Test;

public class Problem297Test {


    @Test
    public void preOrderTraversal() {
        final String string = new Problem297().serialize(node(0, node(1), node(2, node(3), node(4))));
        System.out.println(string);
    }

    @Test
    public void deserialize() {
        final TreeNode tree = new Problem297().deserialize("(0,(1,null,null),(2,(3,null,null),(4,null,null)))");
        System.out.println(tree);
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