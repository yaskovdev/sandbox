package com.yaskovdev;

import org.openjdk.jcstress.annotations.*;
import org.openjdk.jcstress.infra.results.II_Result;

// We should do at least one read before both x and y can be 1, so (1, 1) seems to be impossible.
// But it is only impossible if you declare both x and y volatile.
@JCStressTest
@Outcome(id = "0, 0", expect = Expect.ACCEPTABLE, desc = "Acceptable, sequentially consistent")
@Outcome(id = "1, 0", expect = Expect.ACCEPTABLE, desc = "Acceptable, sequentially consistent")
@Outcome(id = "0, 1", expect = Expect.ACCEPTABLE, desc = "Acceptable, sequentially consistent")
@Outcome(id = "1, 1", expect = Expect.ACCEPTABLE_INTERESTING, desc = "Acceptable, not sequentially consistent, should not happen if properly synchronized")
@State
public class BrownbagSequentiallyConsistentExecutions {

    private int x;
    private int y;

    @Actor
    public void thread1(II_Result r) {
        r.r1 = x;
        y = 1;
    }

    @Actor
    public void thread2(II_Result r) {
        r.r2 = y;
        x = 1;
    }
}
