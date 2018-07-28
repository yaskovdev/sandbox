package days14.string;

/**
 * 1.5 (page 91)
 */
public class OneAway {

    boolean solve(final String a, final String b) {
        if (a.length() == b.length()) {
            boolean diffMet = false;
            for (final char c : b.toCharArray()) {
                if (!a.contains(c + "")) {
                    if (diffMet) {
                        return false;
                    } else {
                        diffMet = true;
                    }
                }
            }
            return true;
        } else {
            final String bigger = a.length() > b.length() ? a : b;
            final String smaller = a.length() > b.length() ? b : a;
            if (bigger.length() - smaller.length() > 1) {
                return false;
            } else {
                for (final char c : smaller.toCharArray()) {
                    if (!bigger.contains(c + "")) {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
