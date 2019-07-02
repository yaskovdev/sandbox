package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;

interface TaskRepository {

    Task findOldestAndLock();

    Task findOldestAndLockWithoutQueryDslAndLock();
}
