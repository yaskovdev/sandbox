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
	public Task createRole(final Task task) {
		return repository.save(task);
	}

	@Override
	public Task reproduceIssue(final String code) {
		logger.info("Going to read for the first time");
		repository.findOneByCode(code);
		taskChangedService.onRoleChanged(code);
		return null;
	}
}
