package other;

import org.junit.Test;

import java.util.List;

import static other.TreeNode.node;

public class ProblemTreeRowMaxTest {

    @Test
    public void test0() {
        final List<Integer> output = new ProblemTreeRowMax().solve(node(0, node(1, node(3), node(4, node(5), node(6))), node(2)));
        System.out.println(output);
    }

}