package other;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;

public class GuaranteedDeadlockWithoutJoin {

    public static void main(final String[] args) {
        final ExecutorService service = Executors.newSingleThreadExecutor();
        service.submit(new Runnable() {
            @Override
            public void run() {
                final Future<?> future = service.submit(new Runnable() {
                    @Override
                    public void run() {
                        System.out.println("internal completed");
                    }
                });
                while (!future.isDone()) {
                    Thread.yield();
                }
                System.out.println("external completed");
            }
        });
    }
}
