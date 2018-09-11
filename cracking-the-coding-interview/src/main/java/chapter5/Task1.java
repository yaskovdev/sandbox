package chapter5;

// TODO: check the solution
class Task1 {

    int solve(final int n, final int m, final int i, final int j) {
        final int mask = (-1 << (j + 1)) + (1 << i) - 1;
        return (n & mask) + (m << i);
    }
}
