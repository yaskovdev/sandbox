package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class LookAndSayTest {

    @Test
    public void test_lookAndSay() {
        assertEquals("111221", new LookAndSay().lookAndSay("1211"));
    }

    @Test
    public void test_lookAndSaySequence() {
        for (final String string : new LookAndSay().lookAndSaySequence(20)) {
            System.out.println(string);
        }
    }
}
