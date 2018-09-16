package other;

import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static other.Interval.interval;

public class Problem253Test {

    @Test
    public void test1() {
        final int rooms = new Problem253().minMeetingRooms(new Interval[]{interval(0, 30), interval(5, 10), interval(15, 20)});
        assertEquals(2, rooms);
    }

    @Test
    public void test2() {
        final int rooms = new Problem253().minMeetingRooms(new Interval[]{interval(7, 10), interval(2, 4)});
        assertEquals(1, rooms);
    }
}
