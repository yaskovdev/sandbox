package qualified;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class First {

    @Test
    public void test7() {
        assertEquals("4###########5616", testedObject().mask("4556364607935616"));
    }

    @Test
    public void test1() {
        assertEquals("4###-####-####-5616", testedObject().mask("4556-3646-0793-5616"));
    }

    @Test
    public void test3() {
        assertEquals("6######5616", testedObject().mask("64607935616"));
    }

    @Test
    public void test4() {
        assertEquals("12345", testedObject().mask("12345"));
    }

    @Test
    public void test6() {
        assertEquals("", testedObject().mask(""));
    }

    @Test
    public void test5() {
        assertEquals("Skippy", testedObject().mask("Skippy"));
    }

    @Test
    public void test2() {
        assertEquals("8-3-12#dskf3  ___ ", testedObject().mask("8-3-123dskf3  ___ "));
    }

    private static First testedObject() {
        return new First();
    }

    public String mask(final String input) {
        int digitsFromBeginningSoFar = 0;
        char lastDigit = 0;
        int lastDigitIndex = -1;
        final StringBuilder builder = new StringBuilder(input.length());
        final char[] charArray = input.toCharArray();
        for (int i = 0; i < charArray.length; i++) {
            final char character = charArray[i];
            if (Character.isDigit(character)) {
                digitsFromBeginningSoFar++;
                lastDigitIndex = i;
                lastDigit = character;
                builder.append(digitsFromBeginningSoFar <= 4 ? character : "#");
            } else {
                builder.append(character);
            }
        }

        if (lastDigitIndex > -1) {
            builder.setCharAt(lastDigitIndex, lastDigit);
        }

        return builder.toString();
    }
}
