package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
class TaskController {

	private final TaskService service;

	@Autowired
	TaskController(final TaskService service) {
		this.service = service;
	}

	@PostMapping("/roles")
	Task createRole(@RequestBody final Task task) {
		return service.createRole(task);
	}

	@GetMapping("/roles/{code}/issue")
	Task reproduceIssue(@PathVariable("code") final String code) {
		return service.reproduceIssue(code);
	}
}
