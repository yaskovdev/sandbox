package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Set;
import java.util.Stack;
import java.util.TreeSet;
import java.util.stream.Collectors;

public class Problem301 {

    private final Set<String> solutions = new TreeSet<>((first, second) -> {
        final int difference = second.length() - first.length();
        return difference == 0 ? first.compareTo(second) : difference;
    });

    List<String> removeInvalidParentheses(String s) {
        solve(s);
        final int length = solutions.iterator().next().length();
        return solutions.stream().filter(solution -> solution.length() == length).collect(Collectors.toList());
    }

    private void solve(String input) {
        if (isValid(input)) {
            solutions.add(input);
        } else {
            final List<String> candidates = withoutOne(input);
            for (final String s : candidates) {
                solve(s);
            }
        }
    }

    private static List<String> withoutOne(String input) {
        if (input.isEmpty()) {
            return Collections.emptyList();
        } else {
            final List<String> outputs = new ArrayList<>();
            for (int i = 0; i < input.length(); i++) {
                outputs.add(input.substring(0, i) + input.substring(i + 1));
            }
            return outputs;
        }
    }

    private static boolean isValid(final String input) {
        final Stack<Character> stack = new Stack<>();
        for (final char c : input.toCharArray()) {
            if (c == '(') {
                stack.push(c);
            } else if (c == ')') {
                if (stack.isEmpty()) {
                    return false;
                }
                final Character top = stack.pop();
                if (top != '(') {
                    return false;
                }
            }
        }
        return stack.isEmpty();
    }
}
