package other;

import org.junit.Test;

public class Problem301Test {

    @Test
    public void test1() {
        System.out.println(new Problem301().removeInvalidParentheses("()())()"));
    }

    @Test
    public void test2() {
        System.out.println(new Problem301().removeInvalidParentheses("(a)())()"));
    }

    @Test
    public void test3() {
        System.out.println(new Problem301().removeInvalidParentheses(")("));
    }

    @Test
    public void test4() {
        System.out.println(new Problem301().removeInvalidParentheses("))(p(((())"));
    }
}
