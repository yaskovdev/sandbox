package other;

public class Problem297 {

    int index = 0;

    String serialize(TreeNode n) {
        if (n == null) {
            return "null";
        }
        final StringBuilder builder = new StringBuilder();
        builder.append("(")
                .append(n.val)
                .append(",")
                .append(n.left == null ? "null" : serialize(n.left))
                .append(",")
                .append(n.right == null ? "null" : serialize(n.right))
                .append(")");
        return builder.toString();
    }

    TreeNode deserialize(String string) {
        return deserializeNode(string);
    }

    private TreeNode deserializeNode(String string) {
        if (string.charAt(index) == '(') {
            index++; // skip open bracket
            int value = deserializeValue(string);
            index++; // skip comma after value
            final TreeNode left = deserializeNode(string);
            index++; // skip comma after left node
            final TreeNode right = deserializeNode(string);
            index++; // skip closing bracket
            final TreeNode node = new TreeNode(value);
            node.left = left;
            node.right = right;
            return node;
        } else {
            index += 4;
            return null;
        }
    }

    private int deserializeValue(String string) {
        int k = 1;
        if (string.charAt(index) == '-') {
            k = -1;
            index++;
        }
        StringBuilder result = new StringBuilder();
        while (Character.isDigit(string.charAt(index))) {
            result.append(string.charAt(index));
            index++;
        }
        return k * Integer.parseInt(result.toString());
    }
}
