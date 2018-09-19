package other;

class Problem29 {

    int divide(final int dividend, final int divisor) {
        long buffer = Math.abs(dividend);
        int result = 0;
        while (buffer >= Math.abs(divisor)) {
            buffer = buffer - Math.abs(divisor);
            result += 1;
        }
        if (dividend >= 0 && divisor > 0) {
            return result;
        } else if (dividend >= 0 && divisor < 0) {
            return -result;
        } else if (dividend < 0 && divisor > 0) {
            return -result;
        } else {
            return result;
        }
    }
}
