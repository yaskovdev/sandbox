package days14.string;

/**
 * 1.9 (page 91)
 */
public class StringRotation {

    public boolean isRotation(final String s1, final String s2) {
        return (s1 + s1).contains(s2);
    }
}
