package com.yaskovdev;

import org.openjdk.jcstress.annotations.*;
import org.openjdk.jcstress.infra.results.II_Result;

@JCStressTest
@Outcome(id = "1, 1", expect = Expect.ACCEPTABLE_INTERESTING, desc = "Each thread sees the same value, regardless of the update. One update is lost.")
@Outcome(id = "1, 2", expect = Expect.ACCEPTABLE, desc = "Each thread sees its own increment, as if the increment is atomic.")
@Outcome(id = "2, 1", expect = Expect.ACCEPTABLE, desc = "Each thread sees its own increment, as if the increment is atomic.")
@State
public class VolatileIncrementAtomicityTest {

    volatile int x;

    @Actor
    public void actor1(II_Result r) {
        r.r1 = ++x;
    }

    @Actor
    public void actor2(II_Result r) {
        r.r2 = ++x;
    }
}
