package other;

import org.junit.Test;

public class Problem237Test {

    @Test
    public void test() {
        final String result = new Problem237().solve(1_234_567_891);
        System.out.println(result);
    }

    @Test
    public void test10() {
        final String result = new Problem237().solve(10);
        System.out.println(result);
    }

    @Test
    public void test100() {
        final String result = new Problem237().solve(100);
        System.out.println(result);
    }

    @Test
    public void test1000() {
        final String result = new Problem237().solve(1000);
        System.out.println(result);
    }

    @Test
    public void test10000() {
        final String result = new Problem237().solve(10_000);
        System.out.println(result);
    }

    @Test
    public void test10001() {
        final String result = new Problem237().solve(10_001);
        System.out.println(result);
    }

    @Test
    public void test10010() {
        final String result = new Problem237().solve(10_010);
        System.out.println(result);
    }

    @Test
    public void test100000() {
        final String result = new Problem237().solve(100_000);
        System.out.println(result);
    }

    @Test
    public void test1_000_000() {
        final String result = new Problem237().solve(1_000_000);
        System.out.println(result);
    }
}
