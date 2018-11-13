package other;

public class Group {

    private final String pattern;

    Group(String pattern) {
        this.pattern = pattern;
    }

    @Override
    public String toString() {
        return "(" + pattern + ")";
    }

    boolean isMatchedBy(String input) {
        if (input.isEmpty()) {
            return true;
        } else if (pattern.endsWith("*")) {
            final char letter = pattern.charAt(0);
            return containsOnly(input, letter);
        } else {
            return equalsRegardingDot(input, pattern);
        }
    }

    private boolean equalsRegardingDot(String input, String pattern) {
        if (input.length() <= pattern.length()) {
            for (int i = 0; i < input.length(); i++) {
                if (pattern.charAt(i) != input.charAt(i) && pattern.charAt(i) != '.') {
                    return false;
                }
            }
            return true;
        } else {
            return false;
        }
    }

    private boolean containsOnly(String input, char letter) {
        if (letter == '.') {
            return true;
        } else {
            for (final char c : input.toCharArray()) {
                if (c != letter) {
                    return false;
                }
            }
            return true;
        }
    }

    boolean isFullyMatchedBy(String input) {
        if (pattern.endsWith("*")) {
            return true;
        } else {
            return pattern.length() == input.length();
        }
    }
}
