package org.example;

import java.util.function.Function;

public class Nothing<T> extends Maybe<T> {

    @Override
    public <U> Monad<U> flatMap(Function<T, Monad<U>> mapper) {
        return new Nothing<>();
    }

    @Override
    public String toString() {
        return "Nothing{}";
    }
}
