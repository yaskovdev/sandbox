package com.yaskovdev;


import org.openjdk.jcstress.annotations.*;
import org.openjdk.jcstress.infra.results.II_Result;

import java.lang.invoke.MethodHandles;
import java.lang.invoke.VarHandle;

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
            y = 1;
        }
    }

    @Actor
    void observer(II_Result r) {
        r.r1 = (int) VH_Y.getOpaque(this);
        r.r2 = (int) VH_X.getOpaque(this);
    }
}
