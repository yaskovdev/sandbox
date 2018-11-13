package other;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import static java.util.Arrays.asList;

class PhoneNumberLetterCombinations {

    private final Map<Character, List<String>> map = map();

    List<String> letterCombinations(final String digits) {
        List<String> solutions = new ArrayList<>();
        solutions.add("");
        for (final char digit : digits.toCharArray()) {
            final List<String> newSolutions = new ArrayList<>();
            for (final String solution : solutions) {
                for (final String letter : map.get(digit)) {
                    newSolutions.add(solution + letter);
                }
            }
            solutions = newSolutions;
        }
        return solutions;
    }

    private static Map<Character, List<String>> map() {
        final Map<Character, List<String>> map = new HashMap<>();
        map.put('2', asList("a", "b", "c"));
        map.put('3', asList("d", "e", "f"));
        map.put('4', asList("g", "h", "i"));
        map.put('5', asList("j", "k", "l"));
        map.put('6', asList("m", "n", "o"));
        map.put('7', asList("p", "q", "r", "s"));
        map.put('8', asList("t", "u", "v"));
        map.put('9', asList("w", "x", "y", "z"));
        return map;
    }
}
