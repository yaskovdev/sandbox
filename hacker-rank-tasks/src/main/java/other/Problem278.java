package other;

public class Problem278 extends VersionControl {

    public int firstBadVersion(final int n) {
        long l = 1;
        long r = n;
        while (l < r) {
            long m = (l + r) / 2;
            if (isBadVersion((int) m)) {
                r = m;
            } else {
                l = m + 1;
            }
        }
        return (int) l;
    }
}
