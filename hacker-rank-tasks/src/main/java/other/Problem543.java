package other;

class Problem543 {

    private int record = 1;

    int diameterOfBinaryTree(final TreeNode root) {
        depth(root);
        return record - 1;
    }

    private int depth(final TreeNode node) {
        if (node == null) {
            return 0;
        }
        final int left = depth(node.left);
        final int right = depth(node.right);
        record = Math.max(record, left + right + 1);
        return Math.max(left, right) + 1;
    }
}
