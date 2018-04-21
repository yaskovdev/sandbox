package qualified;

import org.junit.Test;

import java.util.Stack;

import static org.junit.Assert.assertEquals;

public class Third {

    @Test
    public void test1() {
        assertEquals(0, testedObject().evaluate(""), 0);
    }

    @Test
    public void test2() {
        assertEquals(3, testedObject().evaluate("3 2 1 - *"), 0);
    }

    @Test
    public void test5() {
        assertEquals(3, testedObject().evaluate("3 2        1 - *  "), 0);
    }

    @Test
    public void test7() {
        assertEquals(3.036, testedObject().evaluate("3 2.012 1 - *"), 0);
    }

    @Test
    public void test3() {
        assertEquals(6, testedObject().evaluate("7 4 5 + * 3 - 10 /"), 0);
    }

    @Test
    public void test4() {
        assertEquals(5000.001, testedObject().evaluate("7 4 5000.001"), 0);
    }

    @Test(expected = NumberFormatException.class)
    public void test6() {
        testedObject().evaluate("1 3+");
    }

    private static Third testedObject() {
        return new Third();
    }

    public double evaluate(final String expression) {
        final Stack<Double> stack = new Stack<>();
        final String nonEmptyExpression = expression.isEmpty() ? "0" : expression;
        final String[] tokens = nonEmptyExpression.split("\\s+");
        for (final String token : tokens) {
            if ("+".equals(token)) {
                stack.push(stack.pop() + stack.pop());
            } else if ("-".equals(token)) {
                final Double subtrahend = stack.pop();
                final Double minuend = stack.pop();
                stack.push(minuend - subtrahend);
            } else if ("*".equals(token)) {
                stack.push(stack.pop() * stack.pop());
            } else if ("/".equals(token)) {
                final Double divisor = stack.pop();
                final Double dividend = stack.pop();
                stack.push(dividend / divisor);
            } else {
                stack.push(Double.valueOf(token));
            }
        }
        return stack.pop();
    }
}
