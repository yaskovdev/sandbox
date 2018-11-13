package other;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class EditDistanceTest {

    @Test
    public void test1_oneEditApart() {
        assertFalse(new EditDistance().oneEditApart("cat", "dog"));
    }

    @Test
    public void test2_oneEditApart() {
        assertTrue(new EditDistance().oneEditApart("cat", "cats"));
    }

    @Test
    public void test3_oneEditApart() {
        assertTrue(new EditDistance().oneEditApart("cat", "cut"));
    }

    @Test
    public void test4_oneEditApart() {
        assertTrue(new EditDistance().oneEditApart("cat", "cast"));
    }

    @Test
    public void test5_oneEditApart() {
        assertTrue(new EditDistance().oneEditApart("cat", "at"));
    }

    @Test
    public void test6_oneEditApart() {
        assertFalse(new EditDistance().oneEditApart("cat", "act"));
    }
}
