package other;

class Problem832 {

	int[][] flipAndInvertImage(final int[][] image) {
		final int[][] result = new int[image.length][image[0].length];
		for (int i = 0; i < image.length; i++) {
			for (int j = 0; j < image[i].length; j++) {
				result[i][j] = 1 - image[i][image[j].length - j - 1];
			}
		}
		return result;
	}
}
