package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
class TaskChangedServiceImpl implements TaskChangedService {

    private static final Logger logger = LoggerFactory.getLogger(TaskChangedServiceImpl.class);

    private final TaskRepository taskRepository;

    @Autowired
    TaskChangedServiceImpl(final TaskRepository taskRepository) {
        this.taskRepository = taskRepository;
    }

    @Override
    @Transactional
    public Task onRoleChanged() {
        logger.info("Going to find oldest and lock");
        return taskRepository.findOldestAndLock();
    }
}
