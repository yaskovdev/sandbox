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
		int i;
		for (i = 0; i < elevation.length; i++) {
			if (elevation[i] >= level) {
				break;
			}
		}

		int j;
		for (j = elevation.length - 1; j >= 0; j--) {
			if (elevation[j] >= level) {
				break;
			}
		}

		int count = 0;

		for (int k = i; k <= j; k++) {
			if (elevation[k] < level) {
				count++;
			}
		}
		return count;
	}
}
