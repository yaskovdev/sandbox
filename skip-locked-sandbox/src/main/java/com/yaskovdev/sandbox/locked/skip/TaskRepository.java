package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.transaction.annotation.Transactional;

import static javax.persistence.LockModeType.PESSIMISTIC_WRITE;

interface TaskRepository extends JpaRepository<Task, Integer> {

	@Transactional(readOnly = true)
	Task findOneByCode(final String code);

	@Lock(PESSIMISTIC_WRITE)
	Task findOneAndLockByCode(final String code);
}
