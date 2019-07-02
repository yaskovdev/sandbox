package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;

interface TaskService {

    Task createTask(Task task);

    Task reproduceIssue();
}
