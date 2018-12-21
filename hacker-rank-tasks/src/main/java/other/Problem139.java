package other;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

class Problem139 {

	private final Map<String, Boolean> cache = new HashMap<>();

	boolean wordBreak(final String input, final List<String> dictionary) {
		boolean ok = false;

		if (input.isEmpty()) {
			return true;
		} else if (cache.containsKey(input)) {
			return cache.get(input);
		} else {
			for (final String word : dictionary) {
				if (input.startsWith(word)) {
					ok = ok || wordBreak(input.substring(word.length()), dictionary);
				}
			}

			cache.put(input, ok);

			return ok;
		}
	}
}
