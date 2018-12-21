package other;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class Problem5Test {

	@Test
	public void test() {
		assertEquals("bab", new Problem5().longestPalindrome("babad"));
	}

	@Test
	public void test2() {
		assertEquals("bb", new Problem5().longestPalindrome("cbbd"));
	}
}
