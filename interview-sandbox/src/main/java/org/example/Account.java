package org.example;

import java.math.BigDecimal;
import java.util.concurrent.locks.ReentrantLock;

class Account {

    public final long id;
    public ReentrantLock lock = new ReentrantLock();
    public BigDecimal amount;

    Account(long id) {
        this.id = id;
    }
}
