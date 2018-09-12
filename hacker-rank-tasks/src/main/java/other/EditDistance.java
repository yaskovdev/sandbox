package other;

public class EditDistance {

    boolean oneEditApart(final String first, final String second) {
        if (first.length() == second.length()) {
            return differInExactlyOneCharacter(first, second);
        } else if (Math.abs(first.length() - second.length()) == 1) {
            return first.contains(second) || second.contains(first);
        } else {
            return false;
        }
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
