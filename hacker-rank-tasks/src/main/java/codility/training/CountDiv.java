package codility.training;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

/**
 * https://app.codility.com/programmers/lessons/5-prefix_sums/count_div/
 */
public class CountDiv {

    @Test
    public void test1() {
        assertEquals(3, testedObject().solution(6, 11, 2));
    }

    @Test
    public void test2() {
        assertEquals(2, testedObject().solution(2, 4, 2));
    }

    @Test
    public void test3() {
        assertEquals(1, testedObject().solution(2, 13, 7));
    }

    @Test
    public void test4() {
        assertEquals(7, testedObject().solution(1, 7, 1));
    }

    @Test
    public void test5() {
        assertEquals(0, testedObject().solution(3, 3, 2));
    }

    @Test
    public void test6() {
        assertEquals(0, testedObject().solution(4, 5, 3));
    }

    @Test
    public void test7() {
        assertEquals(1, testedObject().solution(3, 4, 3));
    }

    @Test
    public void test8() {
        assertEquals(1, testedObject().solution(11, 13, 2));
    }

    @Test
    public void test9() {
        assertEquals(1, testedObject().solution(11, 18, 17));
    }

    private static CountDiv testedObject() {
        return new CountDiv();
    }

    public int solution(int a, int b, int k) {
        final int firstDivisible = a % k == 0 ? a : a + (k - a % k);
        final int lastDivisible = b % k == 0 ? b : b - b % k;
        return (lastDivisible - firstDivisible) / k + 1;
    }
}
