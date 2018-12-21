package other;

import java.util.Arrays;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.Objects;
import java.util.Queue;
import java.util.Set;

class Problem200 {

	static class Pair {

		final int x;
		final int y;

		private Pair(int x, int y) {
			this.x = x;
			this.y = y;
		}

		static Pair of(int x, int y) {
			return new Pair(x, y);
		}

		@Override
		public boolean equals(Object o) {
			if (this == o)
				return true;
			if (o == null || getClass() != o.getClass())
				return false;
			Pair pair = (Pair) o;
			return x == pair.x &&
					y == pair.y;
		}

		@Override
		public int hashCode() {
			return Objects.hash(x, y);
		}
	}

	int numIslands(final char[][] grid) {
		int count = 0;
		for (int i = 0; i < grid.length; i++) {
			for (int j = 0; j < grid[i].length; j++) {
				if (grid[i][j] == '1') {
					for (final Pair pair : getIsland(grid, new Pair(i, j))) {
						grid[pair.x][pair.y] = '0';
					}
					count++;
				}
			}
		}
		return count;
	}

	private Set<Pair> getIsland(char[][] grid, final Pair nextLand) {
		final Queue<Pair> queue = new LinkedList<>();
		final Set<Pair> island = new HashSet<>();
		queue.add(nextLand);

		while (!queue.isEmpty()) {
			final Pair land = queue.remove();
			for (final Pair pair : surrounding(land, grid)) {
				if (!island.contains(pair)) {
					island.add(pair);
					queue.add(pair);
				}
			}
		}
		return island;
	}

	private Set<Pair> surrounding(final Pair land, final char[][] grid) {
		final Set<Pair> candidates =
				new HashSet<>(Arrays.asList(Pair.of(land.x, land.y), Pair.of(land.x - 1, land.y), Pair.of(land.x + 1, land.y), Pair.of(land.x, land.y - 1), Pair.of(land.x, land.y + 1)));
		final Set<Pair> result = new HashSet<>();
		for (final Pair candidate : candidates) {
			if (candidate.x >= 0 && candidate.x < grid.length && candidate.y >= 0 && candidate.y < grid[0].length && grid[candidate.x][candidate.y] == '1') {
				result.add(candidate);
			}
		}
		return result;
	}
}
