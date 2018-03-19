package interview;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class First {

    @Test
    public void test1() {
        assertEquals(0, testedObject().solution(new int[]{}));
    }

    private static First testedObject() {
        return new First();
    }

    public int solution(final int[] array) {
        return array.length;
    }
}
