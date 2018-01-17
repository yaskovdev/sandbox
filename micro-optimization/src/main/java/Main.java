public class Main {

    public static void main(String[] args) {
        System.out.println(executionTimeOf(new MicroOptimizedClass(), 100000));
        System.out.println(executionTimeOf(new JustClass(), 100000));
    }

    private static long executionTimeOf(final Calculator calculator, final int n) {
        long sum = 0;
        for (int i = 0; i < n; i++) {
            sum += executionTimeOf(calculator);
        }
        return sum / n;
    }

    private static long executionTimeOf(final Calculator calculator) {
        final long start = System.nanoTime();
        calculator.calculate();
        return System.nanoTime() - start;
    }
}
