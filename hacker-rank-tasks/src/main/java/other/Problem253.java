package other;

import java.util.Set;
import java.util.TreeSet;

class Problem253 {

    int minMeetingRooms(Interval[] intervals) {
        int simultaneous = 0;
        int record = 0;
        Set<Integer> times = new TreeSet<>();
        for (Interval interval : intervals) {
            times.add(interval.start);
            times.add(interval.end);
        }
        for (Integer i : times) {
            simultaneous = simultaneous + startingAt(intervals, i) - endingAt(intervals, i);
            if (simultaneous > record) {
                record = simultaneous;
            }
        }
        return record;
    }

    private int startingAt(Interval[] intervals, int i) {
        int result = 0;
        for (Interval interval : intervals) {
            if (interval.start == i) {
                result++;
            }
        }
        return result;
    }

    private int endingAt(Interval[] intervals, int i) {
        int result = 0;
        for (Interval interval : intervals) {
            if (interval.end == i) {
                result++;
            }
        }
        return result;
    }
}
