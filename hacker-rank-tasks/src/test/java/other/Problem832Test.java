package other;

import org.junit.Test;

public class Problem832Test {

	@Test
	public void test1() {
		final int[][] result = new Problem832().flipAndInvertImage(new int[][] {
				{ 1, 1, 0 },
				{ 1, 0, 1 },
				{ 0, 0, 0 }
		});

		for (int[] row : result) {
			for (int cell : row) {
				System.out.print(cell);
			}
			System.out.println();
		}
	}
}
