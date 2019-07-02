package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Task;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
class TaskController {

    private final TaskChangedService service;

    @Autowired
    TaskController(final TaskChangedService service) {
        this.service = service;
    }

    @GetMapping("/tasks")
    Task reproduceIssue() {
        return service.onRoleChanged();
    }
}
