package other;

import java.util.HashSet;
import java.util.Set;

class Problem804 {

	private static final String[] LETTERS = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..",
			"--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };

	int uniqueMorseRepresentations(String[] words) {
		final Set<String> representations = new HashSet<>();
		for (final String word : words) {
			representations.add(transform(word));
		}
		return representations.size();
	}

	private static String transform(final String word) {
		final StringBuilder builder = new StringBuilder();
		for (char c : word.toCharArray()) {
			builder.append(LETTERS[c - 'a']);
		}
		return builder.toString();
	}
}
