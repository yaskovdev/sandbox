package other;

public class Problem680 {

    public boolean validPalindrome(final String s) {
        return valid(s, true) || valid(s, false);
    }

    private boolean valid(String string, boolean left) {
        boolean fixed = false;
        int p = 0;
        int q = 0;
        for (int i = 0; i < string.length() / 2; i++) {
            if (string.charAt(i + p) != string.charAt(string.length() + q - i - 1)) {
                if (fixed) {
                    return false;
                } else {
                    fixed = true;
                    i--; // try again
                    if (left) {
                        p++;
                    } else {
                        q++;
                    }
                }
            }
        }
        return true;
    }
}
