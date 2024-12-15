package org.example;

import java.util.function.Function;

public class Just<T> extends Maybe<T> {

    private final T value;

    public Just(T value) {
        this.value = value;
    }

    @Override
    public <U> Monad<U> flatMap(Function<T, Monad<U>> mapper) {
        return mapper.apply(value);
    }

    @Override
    public String toString() {
        return "Just{" +
                "value=" + value +
                '}';
    }
}
