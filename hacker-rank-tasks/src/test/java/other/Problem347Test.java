package other;

import org.junit.Test;

import java.util.List;

public class Problem347Test {

    @Test
    public void test0() {
        final List<Integer> output = new Problem347().topKFrequent(new int[]{1, 1, 1, 2, 2, 3}, 2);
        System.out.println(output);
    }

}