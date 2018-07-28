package days14.string;

/**
 * 1.6 (page 91)
 */
public class StringCompression {

    public String compress(final String s) {
        if (s.isEmpty()) {
            return s;
        }
        final StringBuilder b = new StringBuilder();
        int count = 0;
        char current = s.charAt(0);
        b.append(current);
        for (final char c : s.toCharArray()) {
            if (current == c) {
                count++;
            } else {
                b.append(count);
                count = 1;
                current = c;
                b.append(current);
            }
        }
        b.append(count);
        return s.length() > b.length() ? b.toString() : s;
    }
}
