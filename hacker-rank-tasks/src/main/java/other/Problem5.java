package other;

import java.util.HashMap;
import java.util.Map;

class Problem5 {

	private final Map<String, Boolean> cache = new HashMap<>();

	String longestPalindrome(String string) {
		String result = "";
		for (int i = 0; i < string.length(); i++) {
			for (int j = i; j < string.length(); j++) {
				final String substring = string.substring(i, j + 1);
				if (substring.length() > result.length() && palindrome(substring)) {
					result = substring;
				}
			}
		}
		return result;
	}

	private boolean palindrome(final String string) {
		if (cache.containsKey(string)) {
			return cache.get(string);
		} else {
			for (int i = 0; i < string.length() / 2; i++) {
				if (string.charAt(i) != string.charAt(string.length() - i - 1)) {
					cache.put(string, false);
					return false;
				}
			}
			cache.put(string, true);
			return true;
		}
	}
}
