package other;

import java.util.Stack;

class Problem20 {

	boolean isValid(final String s) {
		final Stack<Character> stack = new Stack<>();

		for (final char c : s.toCharArray()) {
			if (c == '(' || c == '{' || c == '[') {
				stack.push(c);
			} else {
				if (stack.isEmpty()) {
					return false;
				}
				final Character openBracket = stack.pop();
				if (!match(openBracket, c)) {
					return false;
				}
			}
		}

		return stack.isEmpty();
	}

	private boolean match(char open, Character close) {
		return open == '(' && close == ')' || open == '{' && close == '}' || open == '[' && close == ']';
	}
}
