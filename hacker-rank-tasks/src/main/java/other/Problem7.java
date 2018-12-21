package other;

class Problem7 {

    int reverse(final int x) {
        final String string = Math.abs(x) + "";
        final StringBuilder builder = new StringBuilder(string);
        final StringBuilder reversed = builder.reverse();
        try {
            final int output = Integer.parseInt(reversed.toString());
            return x >= 0 ? output : -output;
        } catch (final NumberFormatException e) {
            return 0;
        }
    }
}
