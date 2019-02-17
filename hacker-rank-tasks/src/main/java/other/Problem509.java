package other;

class Solution {

    public int fib(int N) {
        return N == 0 || N == 1 ? N : fib(N - 1) + fib(N - 2);
    }
}
