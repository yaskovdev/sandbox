package com.yaskovdev.sandbox.locked.skip;

import com.querydsl.jpa.impl.JPAQueryFactory;
import com.yaskovdev.sandbox.locked.skip.model.Task;
import org.springframework.stereotype.Component;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import static com.yaskovdev.sandbox.locked.skip.TaskStatus.NOT_STARTED;
import static com.yaskovdev.sandbox.locked.skip.model.QTask.task;
import static javax.persistence.LockModeType.PESSIMISTIC_WRITE;
import static org.hibernate.LockOptions.NO_WAIT;
import static org.hibernate.LockOptions.SKIP_LOCKED;

@Component
public class TaskRepositoryImpl implements TaskRepository {

    @PersistenceContext
    private EntityManager manager;

    @Override
    public Task findOldestAndLock() {
        final JPAQueryFactory factory = new JPAQueryFactory(manager);
        return factory.selectFrom(task)
                .orderBy(task.created.desc())
                .setLockMode(PESSIMISTIC_WRITE)
                .setHint("javax.persistence.lock.timeout", NO_WAIT)
                .fetchFirst();
    }

    @Override
    public Task findOldestAndLockWithoutQueryDslAndLock() {
        return manager.createQuery("select t from Task t where t.status = :status order by t.created", Task.class)
                .setParameter("status", NOT_STARTED)
                .setLockMode(PESSIMISTIC_WRITE)
                .setFirstResult(0)
                .setMaxResults(2)
                .setHint("javax.persistence.lock.timeout", SKIP_LOCKED)
                .getResultList().get(0);
    }
}
