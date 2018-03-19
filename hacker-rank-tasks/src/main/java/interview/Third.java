package interview;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class Third {

    @Test
    public void test1() {
        assertEquals(100, testedObject().solution(new int[]{}));
    }

    private static Third testedObject() {
        return new Third();
    }

    public int solution(final int[] array) {
        return array.length;
    }
}
