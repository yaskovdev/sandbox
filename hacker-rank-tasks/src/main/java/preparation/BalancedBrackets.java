package preparation;

import org.junit.Assert;
import org.junit.Test;

import java.util.Stack;

public class BalancedBrackets {

    @Test
    public void test0() {
        Assert.assertEquals("YES", solution("({}[])"));
    }

    @Test
    public void test1() {
        Assert.assertEquals("NO", solution("({[}])"));
    }

    @Test
    public void test2() {
        Assert.assertEquals("YES", solution(""));
    }

    @Test
    public void test3() {
        Assert.assertEquals("NO", solution("["));
    }

    @Test
    public void test4() {
        Assert.assertEquals("NO", solution("]"));
    }

    private static String solution(final String expression) {
        final Stack<Character> stack = new Stack<>();
        for (final char character : expression.toCharArray()) {
            if (character == '(' || character == '[' || character == '{') {
                stack.push(character);
            } else {
                if (stack.isEmpty()) {
                    return "NO";
                }
                final Character bracket = stack.pop();
                if (!(bracket == '(' && character == ')' || bracket == '[' && character == ']' || bracket == '{' && character == '}')) {
                    return "NO";
                }
            }
        }
        return stack.isEmpty() ? "YES" : "NO";
    }
}
