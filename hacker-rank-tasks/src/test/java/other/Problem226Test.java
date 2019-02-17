package other;

import static org.junit.Assert.assertEquals;
import static other.TreeNode.node;

import org.junit.Test;

public class Problem226Test {

	@Test
	public void test() {
		final TreeNode node = new Problem226().invertTree(node(1));
		assertEquals(1, node.val);
	}

	@Test
	public void test2() {
		final TreeNode node = new Problem226().invertTree(node(1, node(2), node(3)));
		assertEquals(1, node.val);
		assertEquals(3, node.left.val);
		assertEquals(2, node.right.val);
	}
}
