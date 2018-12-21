package other;

class Problem42 {

	int trap(final int[] elevation) {
		int result = 0;

		for (int i = 1; i <= max(elevation); i++) {
			result += countHolesAtLevel(elevation, i);
		}

		return result;
	}

	private static int max(final int[] elevation) {
		int max = Integer.MIN_VALUE;
		for (final int value : elevation) {
			if (value > max) {
				max = value;
			}
		}
		return max;
	}

	private static int countHolesAtLevel(final int[] elevation, final int level) {
		int result = 0;
		int count = 0;

		boolean hole = false;

		for (final Integer height : elevation) {
			if (height >= level) {
				if (hole) {
					hole = false;
					result += count;
					count = 0;
				} else {
					hole = true;
				}
			} else if (hole) {
				count++;
			}
		}

		return result;
	}
}
