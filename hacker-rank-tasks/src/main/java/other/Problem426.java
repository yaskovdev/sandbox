package other;

class Problem426 {

    Node treeToDoublyList(Node root) {
        return root == null ? null : traverse(root);
    }

    private Node traverse(Node root) {
        if (root.left != null) {
            Node left = traverseLeft(root.left);
            root.left = left;
            left.right = root;
        }
        if (root.right != null) {
            Node right = traverseRight(root.right);
            root.right = right;
            right.left = root;
        }
        Node begin = root;
        while (begin.left != null) {
            begin = begin.left;
        }
        Node end = root;
        while (end.right != null) {
            end = end.right;
        }
        end.right = begin;
        begin.left = end;
        return begin;
    }

    private Node traverseLeft(Node node) {
        if (node.left != null) {
            Node left = traverseLeft(node.left);
            node.left = left;
            left.right = node;
        }
        if (node.right != null) {
            Node right = traverseRight(node.right);
            node.right = right;
            right.left = node;
        }
        Node pointer = node;
        while (pointer.right != null) {
            pointer = pointer.right;
        }
        return pointer;
    }

    private Node traverseRight(Node node) {
        if (node.left != null) {
            Node left = traverseLeft(node.left);
            node.left = left;
            left.right = node;
        }
        if (node.right != null) {
            Node right = traverseRight(node.right);
            node.right = right;
            right.left = node;
        }
        Node pointer = node;
        while (pointer.left != null) {
            pointer = pointer.left;
        }
        return pointer;
    }
}
