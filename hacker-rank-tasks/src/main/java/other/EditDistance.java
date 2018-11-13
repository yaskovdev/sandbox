package other;

public class EditDistance {

    boolean oneEditApart(final String first, final String second) {
        if (first.length() == second.length()) {
            return differInExactlyOneCharacter(first, second);
        } else if (Math.abs(first.length() - second.length()) == 1) {
            return first.length() > second.length() ? substring(first, second) : substring(second, first);
        } else {
            return false;
        }
    }

    private boolean substring(final String longer, final String shorter) {
        for (char character : shorter.toCharArray()) {
            if (!longer.contains(character + "")) {
                return false;
            }
        }
        return true;
    }

    private boolean differInExactlyOneCharacter(final String first, final String second) {
        boolean thereWasOneDifference = false;
        for (int i = 0; i < first.length(); i++) {
            if (first.charAt(i) != second.charAt(i)) {
                if (thereWasOneDifference) {
                    return false;
                } else {
                    thereWasOneDifference = true;
                }
            }
        }
        return true;
    }
}
