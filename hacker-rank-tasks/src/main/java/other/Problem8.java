package other;

class Problem8 {

    int myAtoi(String s) {
        int k = 1;
        int result = 0;
        boolean numberStarted = false;
        for (final char c : s.toCharArray()) {
            if (Character.isWhitespace(c)) {
                if (numberStarted) {
                    return k * result;
                }
            } else if (c == '+' || c == '-') {
                if (numberStarted) {
                    return k * result;
                } else {
                    numberStarted = true;
                    if (c == '-') {
                        k = -1;
                    }
                }
            } else if (Character.isDigit(c)) {
                try {
                    result = Math.multiplyExact(result, 10);
                    result = Math.addExact(result, c - '0');
                } catch (ArithmeticException e) {
                    return k == 1 ? Integer.MAX_VALUE : Integer.MIN_VALUE;
                }
                numberStarted = true;
            } else {
                return k * result;
            }
        }
        return k * result;
    }
}
