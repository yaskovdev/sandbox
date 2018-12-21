package other;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import org.junit.Test;

public class Problem20Test {

	@Test
	public void test() {
		assertFalse(new Problem20().isValid("(("));
	}

	@Test
	public void test2() {
		assertFalse(new Problem20().isValid("])"));
	}

	@Test
	public void test3() {
		assertTrue(new Problem20().isValid("()"));
	}

	@Test
	public void test4() {
		assertTrue(new Problem20().isValid("()[]{}"));
	}

	@Test
	public void test5() {
		assertFalse(new Problem20().isValid("(]"));
	}

	@Test
	public void test6() {
		assertFalse(new Problem20().isValid("([)]"));
	}

	@Test
	public void test7() {
		assertTrue(new Problem20().isValid("{[]}"));
	}
}