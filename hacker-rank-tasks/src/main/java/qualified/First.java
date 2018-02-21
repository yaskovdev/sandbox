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
        assertEquals("8-#-123dskf3  ___ ", testedObject().mask("8-3-123dskf3  ___ "));
    }

    @Test
    public void test8() {
        assertEquals("A###-####-####-ABCD", testedObject().mask("A234-2345-3456-ABCD"));
    }

    private static First testedObject() {
        return new First();
    }

    public String mask(final String creditCardNumber) {
        final int totalNumberOfDigits = totalNumberOfDigitsIn(creditCardNumber);
        final StringBuilder builder = new StringBuilder(creditCardNumber.length());
        int digitsPassed = -1;
        for (final char character : creditCardNumber.toCharArray()) {
            if (Character.isDigit(character)) {
                digitsPassed++;
                final int digitsLeft = totalNumberOfDigits - digitsPassed;
                builder.append(digitsPassed > 0 && digitsLeft > 4 ? '#' : character);
            } else {
                builder.append(character);
            }
        }

        return builder.toString();
    }

    private static int totalNumberOfDigitsIn(final String input) {
        int totalNumberOfDigits = 0;
        for (final char character : input.toCharArray()) {
            if (Character.isDigit(character)) {
                totalNumberOfDigits++;
            }
        }
        return totalNumberOfDigits;
    }
}
