package com.yaskovdev;

import org.openjdk.jcstress.annotations.*;
import org.openjdk.jcstress.infra.results.II_Result;

// r1 means whether we entered the "if" or not. r2 means the value of p inside the "if".
// (1, 0) is not expected, i.e., we do not expect to see p = 0 inside the "if". Though due to the data race it's possible.
// To fix the data race, you can make "disposed" volatile.
@JCStressTest
@Outcome(id = "0, 0", expect = Expect.ACCEPTABLE, desc = "Acceptable")
@Outcome(id = "1, 0", expect = Expect.FORBIDDEN, desc = "Acceptable")
@Outcome(id = "1, 1", expect = Expect.ACCEPTABLE, desc = "Acceptable")
@State
public class BrownbagDataRace {

    private int p;
    private int disposed; // Add volatile to fix the data race.

    @Actor
    public void thread1(II_Result r) {
        p = 1;
        disposed = 1;
    }

    @Actor
    public void thread2(II_Result r) {
        if (disposed == 1) {
            r.r1 = 1;
            r.r2 = p;
        }
    }
}
