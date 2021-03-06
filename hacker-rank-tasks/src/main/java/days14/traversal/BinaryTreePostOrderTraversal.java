package days14.traversal;

import days14.Node;

import static java.util.Objects.isNull;

public class BinaryTreePostOrderTraversal {

    public static void main(String[] args) {
        final Node tree = new Node(new Node(new Node(1), new Node(2), 3), new Node(4), 5);
        traverse(tree);
    }

    static void traverse(Node node) {
        if (!isNull(node)) {
            traverse(node.left);
            traverse(node.right);
            System.out.println(node.data);
        }
    }
}
