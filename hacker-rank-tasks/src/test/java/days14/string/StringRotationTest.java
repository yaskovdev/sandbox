package days14.string;

import org.junit.Test;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

public class StringRotationTest {

    @Test
    public void test1() {
        assertTrue(new StringRotation().isRotation("waterbottle", "erbottlewat"));
    }

    @Test
    public void test2() {
        assertTrue(new StringRotation().isRotation("erbottlewat", "waterbottle"));
    }

    @Test
    public void test3() {
        assertFalse(new StringRotation().isRotation("pipka", "pakpi"));
    }
}
