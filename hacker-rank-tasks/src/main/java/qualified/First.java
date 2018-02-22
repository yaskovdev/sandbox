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
    public void test9() {
        assertEquals("1#3?45", testedObject().mask("123?45"));
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
        assertEquals("8-#-###dskf#  ___ ", testedObject().mask("8-3-123dskf3  ___ "));
    }

    @Test
    public void test8() {
        assertEquals("A###-####-####-ABCD", testedObject().mask("A234-2345-3456-ABCD"));
    }

    private static First testedObject() {
        return new First();
    }

    public String mask(final String creditCardNumber) {
        final StringBuilder builder = new StringBuilder(creditCardNumber.length());
        for (int i = 0; i < creditCardNumber.length(); i++) {
            final char character = creditCardNumber.charAt(i);
            builder.append(i > 0 && i < creditCardNumber.length() - 4 && Character.isDigit(character) ? '#' : character);
        }
        return builder.toString();
    }
}
