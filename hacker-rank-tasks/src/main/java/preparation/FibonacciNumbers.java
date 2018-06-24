package preparation;

import org.junit.Assert;
import org.junit.Test;

public class FibonacciNumbers {

    @Test
    public void test0() {
        Assert.assertEquals(1, fibonacci(2));
    }

    @Test
    public void test1() {
        Assert.assertEquals(3, fibonacci(4));
    }

    public static int fibonacci(int n) {
        if (n == 0) {
            return 0;
        } else if (n == 1) {
            return 1;
        } else {
            return fibonacci(n - 1) + fibonacci(n - 2);
        }
    }
}
