package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;

interface TaskService {

    Task createRole(Task task);

    Task reproduceIssue(final String code);
}
