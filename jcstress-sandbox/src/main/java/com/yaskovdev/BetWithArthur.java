package com.yaskovdev;


import org.openjdk.jcstress.annotations.*;
import org.openjdk.jcstress.infra.results.II_Result;

import java.lang.invoke.MethodHandles;
import java.lang.invoke.VarHandle;

/**
 * <a href="https://shipilev.net/blog/2016/close-encounters-of-jmm-kind/">This article</a> claims that "synchronized
 * sections should execute in an order consistent with a program order" is false and uses the following test to prove it.
 * Thanks to Arthur for pointing out that the test is incorrect. See below why.
 */
@JCStressTest
@Outcome(id = "0, 0", expect = Expect.ACCEPTABLE)
@Outcome(id = "0, 1", expect = Expect.ACCEPTABLE)
@Outcome(id = "1, 0", expect = Expect.ACCEPTABLE_INTERESTING)
@Outcome(id = "1, 1", expect = Expect.ACCEPTABLE)
@State
public class BetWithArthur {

    private static final VarHandle VH_X, VH_Y;

    static {
        try {
            VH_X = MethodHandles.lookup().findVarHandle(BetWithArthur.class,
                    "x", int.class);
            VH_Y = MethodHandles.lookup().findVarHandle(BetWithArthur.class,
                    "y", int.class);
        } catch (Exception e) {
            throw new IllegalStateException(e);
        }
    }

    int x, y;

    @Actor
    void actor() {
        synchronized (this) {
            x = 1;
        }
        synchronized (this) {
            y = 1;
        }
    }

    // The original code from the article. The problem with this implementation of the observer is that it contains data races.
    // Even if you replace getOpaque with getVolatile (as it was done by the author of the article in [1]), the test
    // is still far from proving the statement: in practice, if you write under the synchronized block, you also read
    // under the same synchronized block.
    //
    // [1] https://github.com/openjdk/jcstress/blob/4434266ec6c3bb5f98567d9d069cef39f7b1609c/jcstress-samples/src/main/java/org/openjdk/jcstress/samples/jmm/advanced/AdvancedJMM_01_SynchronizedBarriers.java#L105-L106
    @Actor
    public void observer(II_Result r) {
        r.r1 = (int) VH_Y.getOpaque(this);
        r.r2 = (int) VH_X.getOpaque(this);
    }

    // Below is the code that would actually prove the statement, but when I run it, the "1, 0" outcome is never observed.
    /*
    @Actor
    void observer(II_Result r) {
        synchronized (this) {
            r.r1 = (int) VH_Y.getOpaque(this);
        }
        synchronized (this) {
            r.r2 = (int) VH_X.getOpaque(this);
        }
    }
    */
}
