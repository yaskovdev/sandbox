package other;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

class Problem17 {

	private final Map<Character, List<Character>> map = map();

	List<String> letterCombinations(final String digits) {
		List<String> result = Collections.singletonList("");

		for (final char digit : digits.toCharArray()) {
			List<String> newResult = new ArrayList<>();
			for (final String combination : result) {
				final List<Character> letters = map.get(digit);
				for (final Character letter : letters) {
					newResult.add(combination + letter);
				}
			}
			result = newResult;
		}
		return result.size() == 1 && "".equals(result.get(0)) ? Collections.emptyList() : result;
	}

	private static Map<Character, List<Character>> map() {
		final Map<Character, List<Character>> map = new HashMap<>();
		map.put('0', Collections.emptyList());
		map.put('1', Collections.emptyList());
		map.put('2', Arrays.asList('a', 'b', 'c'));
		map.put('3', Arrays.asList('d', 'e', 'f'));
		map.put('4', Arrays.asList('g', 'h', 'i'));
		map.put('5', Arrays.asList('j', 'k', 'l'));
		map.put('6', Arrays.asList('m', 'n', 'o'));
		map.put('7', Arrays.asList('p', 'q', 'r', 's'));
		map.put('8', Arrays.asList('t', 'u', 'v'));
		map.put('9', Arrays.asList('w', 'x', 'y', 'z'));
		return map;
	}
}
