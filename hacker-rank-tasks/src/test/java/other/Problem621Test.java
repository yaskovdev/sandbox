package other;

import org.junit.Test;

public class Problem621Test {

    @Test
    public void test() {
        final int time = new Problem621().leastInterval(new char[]{'A', 'A', 'A', 'B', 'B', 'B'}, 2);
        System.out.println(time);
    }
}