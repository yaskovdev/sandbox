package other;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Objects;
import java.util.Set;

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
        if (input.isEmpty()) {
            return Collections.emptyList();
        }
        Set<IntervalWithEquals> intervals = new HashSet<>();
        for (Interval interval : input) {
            intervals.add(new IntervalWithEquals(interval.start, interval.end));
        }
        while (true) {
            Set<IntervalWithEquals> intermediate = new HashSet<>();
            int before = intervals.size();
            for (IntervalWithEquals interval : intervals) {
                Set<IntervalWithEquals> overlapping = intersect(intervals, interval);
                IntervalWithEquals merged = merge(overlapping);
                intermediate.add(merged);
            }
            intervals = intermediate;
            if (intervals.size() == before) {
                break;
            }
        }
        List<Interval> output = new ArrayList<>(intervals.size());
        for (IntervalWithEquals interval : intervals) {
            output.add(new Interval(interval.start, interval.end));
        }
        return output;
    }

    private IntervalWithEquals merge(Set<IntervalWithEquals> overlapping) {
        int min = Integer.MAX_VALUE;
        int max = Integer.MIN_VALUE;
        for (IntervalWithEquals interval : overlapping) {
            if (interval.start < min) {
                min = interval.start;
            }
            if (interval.end > max) {
                max = interval.end;
            }
        }
        return new IntervalWithEquals(min, max);
    }

    private Set<IntervalWithEquals> intersect(Set<IntervalWithEquals> intervals, IntervalWithEquals interval) {
        Set<IntervalWithEquals> intersect = new HashSet<>();
        for (IntervalWithEquals candidate : intervals) {
            if (candidate.start <= interval.start && candidate.end >= interval.start
                    || candidate.start <= interval.end && candidate.end >= interval.end
                    || candidate.start >= interval.start && candidate.end <= interval.end
                    || candidate.start <= interval.start && candidate.end >= interval.end) {
                intersect.add(candidate);
            }
        }
        return intersect;
    }
}
