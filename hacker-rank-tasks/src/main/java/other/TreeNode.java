package other;

class TreeNode {
    int val;
    TreeNode left;
    TreeNode right;

    TreeNode(int x) {
        val = x;
    }

    public static TreeNode node(int value) {
        return node(value, null, null);
    }

    public static TreeNode node(int value, TreeNode left, TreeNode right) {
        final TreeNode node = new TreeNode(value);
        node.left = left;
        node.right = right;
        return node;
    }
}
