package other;

public class Interval {
    int start;
    int end;

    Interval() {
        start = 0;
        end = 0;
    }

    Interval(int s, int e) {
        start = s;
        end = e;
    }

    public static Interval interval(int s, int e) {
        return new Interval(s, e);
    }
}
