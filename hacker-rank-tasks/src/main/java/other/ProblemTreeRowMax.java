package other;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ProblemTreeRowMax {

    private final Map<Integer, List<Integer>> map = new HashMap<>();

    List<Integer> solve(final TreeNode node) {
        final List<Integer> output = new ArrayList<>();
        traverse(node, 0);
        for (int i = 0; i < map.size(); i++) {
            output.add(map.get(i).get(0));
        }
        return output;
    }

    private void traverse(final TreeNode node, final int level) {
        if (node != null) {
            save(level, node.val);

            if (node.left != null) {
                traverse(node.left, level + 1);
            }
            if (node.right != null) {
                traverse(node.right, level + 1);
            }
        }
    }

    private void save(int level, int val) {
        final List<Integer> values = map.computeIfAbsent(level, key -> new ArrayList<>());
        values.add(val);
    }
}
