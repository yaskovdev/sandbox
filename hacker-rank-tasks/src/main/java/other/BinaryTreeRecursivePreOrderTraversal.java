package other;

import java.util.ArrayList;
import java.util.List;

class BinaryTreeRecursivePreOrderTraversal {

    List<Integer> preOrderTraversal(TreeNode root) {
        List<Integer> result = new ArrayList<>();
        result.add(root.val);
        if (root.left != null) {
            result.addAll(preOrderTraversal(root.left));
        }
        if (root.right != null) {
            result.addAll(preOrderTraversal(root.right));
        }
        return result;
    }
}
