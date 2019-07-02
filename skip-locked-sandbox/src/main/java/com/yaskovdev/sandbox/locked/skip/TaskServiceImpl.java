package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
class TaskServiceImpl implements TaskService {

    private static final Logger logger = LoggerFactory.getLogger(TaskServiceImpl.class);

    private final TaskRepository repository;
    private final TaskChangedService taskChangedService;

    @Autowired
    TaskServiceImpl(final TaskRepository repository, final TaskChangedService taskChangedService) {
        this.repository = repository;
        this.taskChangedService = taskChangedService;
    }

    @Override
    public Task createTask(final Task task) {
        return null;
    }

    @Override
    public Task reproduceIssue() {
        return taskChangedService.onRoleChanged();
    }
}
