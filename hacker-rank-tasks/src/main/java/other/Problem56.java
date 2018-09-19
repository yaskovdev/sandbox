package other;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Objects;
import java.util.Queue;

class Problem56 {

    private static class IntervalWithEquals {

        int start;
        int end;

        IntervalWithEquals(int s, int e) {
            this.start = s;
            this.end = e;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;
            IntervalWithEquals interval = (IntervalWithEquals) o;
            return start == interval.start &&
                    end == interval.end;
        }

        @Override
        public int hashCode() {
            return Objects.hash(start, end);
        }

        @Override
        public String toString() {
            return "[" + start + "," + end + "]";
        }
    }

    List<Interval> merge(List<Interval> input) {
        List<Interval> output = new ArrayList<>();
        Queue<Integer> events = new LinkedList<>();
        for (Interval interval : input) {
            events.add(interval.start);
            events.add(interval.end);
        }
        while (!events.isEmpty()) {
            final Integer event = events.remove();
            final List<Interval> overlapping = intersect(output, event);
            if (overlapping.size() > 1) {
                final Interval merge = mergeOverlapping(overlapping);
                events.add(merge.start);
                events.add(merge.end);
            }
        }
        return output;
    }

    private Interval mergeOverlapping(List<Interval> overlapping) {
        int min = Integer.MAX_VALUE;
        int max = Integer.MIN_VALUE;
        for (Interval interval : overlapping) {
            if (interval.start < min) {
                min = interval.start;
            }
            if (interval.end > max) {
                max = interval.end;
            }
        }
        return new Interval(min, max);
    }

    private List<Interval> intersect(List<Interval> intervals, int event) {
        List<Interval> intersect = new ArrayList<>();
        for (Interval candidate : intervals) {
            if (candidate.start <= event && candidate.end >= event) {
                intersect.add(candidate);
            }
        }
        return intersect;
    }
}
