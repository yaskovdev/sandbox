package org.example;

import java.math.BigDecimal;
import java.util.concurrent.locks.ReentrantLock;

class NotEnoughMoneyException extends RuntimeException {

}

class TransferToYourselfException extends RuntimeException {

}

class BalanceTransferService {

    // 2 -> 1
    void transfer(Account from, Account to, BigDecimal amount) {
        if (from.id == to.id) {
            throw new TransferToYourselfException();
        }
        if (amount.compareTo(BigDecimal.ZERO) <= 0) {
            throw new IllegalArgumentException("");
        }

        Account first = from.id < to.id ? from : to;
        Account second = first.id == from.id ? to : from;
        transferInternal(from, to, first.lock, second.lock, amount);
    }

    private void transferInternal(Account from, Account to, ReentrantLock firstLock, ReentrantLock secondLock, BigDecimal amount) {
        firstLock.lock();
        try {
            secondLock.lock();
            try {
                if (from.amount.compareTo(amount) >= 0) {
                    from.amount = from.amount.subtract(amount);
                    to.amount = to.amount.add(amount);
                } else {
                    throw new NotEnoughMoneyException(); // TODO: message
                }
            } finally {
                secondLock.unlock();
            }
        } finally {
            firstLock.unlock();
        }
    }
}
