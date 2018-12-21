package other;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class Problem42Test {

	@Test
	public void test() {
		assertEquals(6, new Problem42().trap(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));
	}

	@Test
	public void test2() {
		assertEquals(10, new Problem42().trap(new int[] { 5, 0, 0, 5 }));
	}

	@Test
	public void test3() {
		assertEquals(1, new Problem42().trap(new int[] { 4, 2, 3 }));
	}

	@Test
	public void test4() {
		assertEquals(3, new Problem42().trap(new int[] { 2, 1, 0, 2 }));
	}
}
