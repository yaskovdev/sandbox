package other;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class Problem200Test {

	@Test
	public void test() {
		final int number = new Problem200().numIslands(new char[][] {
				{ '1', '1', '1', '1', '0' },
				{ '1', '1', '0', '1', '0' },
				{ '1', '1', '0', '0', '0' },
				{ '0', '0', '0', '0', '0' },
		});
		assertEquals(1, number);
	}

	@Test
	public void test2() {
		final int number = new Problem200().numIslands(new char[][] {
				{ '1', '1', '0', '0', '0' },
				{ '1', '1', '0', '0', '0' },
				{ '0', '0', '1', '0', '0' },
				{ '0', '0', '0', '1', '1' },
		});
		assertEquals(3, number);
	}

	@Test
	public void test3() {
		final int number = new Problem200().numIslands(new char[][] {
				{ '1', '1', '1', '1', '1' },
				{ '1', '1', '1', '1', '1' },
				{ '1', '1', '1', '1', '1' },
				{ '1', '1', '1', '1', '1' },
				{ '1', '1', '1', '1', '1' }
		});
		assertEquals(1, number);
	}

	@Test
	public void test4() {
		final int number = new Problem200().numIslands(new char[][] {
				{ '0', '0', '0', '0', '0' },
				{ '0', '0', '0', '0', '0' },
				{ '0', '0', '0', '0', '0' },
				{ '0', '0', '0', '0', '0' }
		});
		assertEquals(0, number);
	}

	@Test
	public void test5() {
		final int number = new Problem200().numIslands(new char[][] {
				{ '0' }
		});
		assertEquals(0, number);
	}
}
