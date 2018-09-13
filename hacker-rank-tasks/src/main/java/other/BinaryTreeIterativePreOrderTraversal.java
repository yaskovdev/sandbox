package other;

import java.util.ArrayList;
import java.util.List;
import java.util.Stack;

public class BinaryTreeIterativePreOrderTraversal {

    List<Integer> preOrderTraversal(final TreeNode root) {
        List<Integer> result = new ArrayList<>();
        Stack<TreeNode> stack = new Stack<>();
        stack.push(root);
        while (!stack.isEmpty()) {
            final TreeNode node = stack.pop();
            result.add(node.val);
            if (node.right != null) {
                stack.push(node.right);
            }
            if (node.left != null) {
                stack.push(node.left);
            }
        }
        return result;
    }
}
