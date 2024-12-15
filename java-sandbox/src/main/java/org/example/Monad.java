package org.example;

import java.util.function.Function;

public interface Monad<T> {
    <U> Monad<U> flatMap(Function<T, Monad<U>> mapper);

    static <T> Monad<T> unit(T value) {
        // Default implementation can throw or provide a fallback.
        throw new UnsupportedOperationException("Not implemented");
    }
}
`