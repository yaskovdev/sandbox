package other;

class Problem253 {

    int minMeetingRooms(Interval[] intervals) {
        int simultaneous = 0;
        int record = 0;
        int min = min(intervals);
        int max = max(intervals);
        for (int i = min; i <= max; i++) {
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

    private int min(Interval[] intervals) {
        int record = Integer.MAX_VALUE;
        for (Interval interval : intervals) {
            if (interval.start < record) {
                record = interval.start;
            }
        }
        return record;
    }

    private int max(Interval[] intervals) {
        int record = Integer.MIN_VALUE;
        for (Interval interval : intervals) {
            if (interval.end > record) {
                record = interval.end;
            }
        }
        return record;
    }
}
