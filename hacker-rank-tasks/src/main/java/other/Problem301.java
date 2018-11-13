package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Set;
import java.util.Stack;

class Problem301 {

    List<String> removeInvalidParentheses(String input) {
        final Set<String> visited = new HashSet<>();
        final Set<String> solutions = new HashSet<>();
        final Queue<String> queue = new LinkedList<>();
        queue.add(input);
        visited.add(input);
        while (!queue.isEmpty()) {
            final String candidate = queue.remove();
            if (isValid(candidate)) {
                solutions.add(candidate);
            } else if (solutions.isEmpty()) {
                for (String c : withoutOneBracket(candidate)) {
                    if (!visited.contains(c)) {
                        queue.add(c);
                        visited.add(c);
                    }
                }
            }
        }
        return new ArrayList<>(solutions);
    }

    private static List<String> withoutOneBracket(String input) {
        if (input.isEmpty()) {
            return Collections.emptyList();
        } else {
            final List<String> outputs = new ArrayList<>();
            for (int i = 0; i < input.length(); i++) {
                if (input.charAt(i) == '(' || input.charAt(i) == ')') {
                    outputs.add(input.substring(0, i) + input.substring(i + 1));
                }
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
