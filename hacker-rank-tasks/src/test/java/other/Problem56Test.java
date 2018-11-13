package other;

import org.junit.Test;

import java.util.Arrays;
import java.util.List;

public class Problem56Test {

    @Test
    public void test1() {
        final List<Interval> result = new Problem56().merge(Arrays.asList(interval(1, 3), interval(2, 6), interval(8, 10), interval(15, 18)));
        System.out.println(result);
    }

    @Test
    public void test2() {
        final List<Interval> result = new Problem56().merge(Arrays.asList(interval(1, 4), interval(4, 5)));
        System.out.println(result);
    }

    private static Interval interval(int start, int end) {
        return new Interval(start, end);
    }
}
