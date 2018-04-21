package com.yaskovdev.sandbox.spring.data;

import com.yaskovdev.sandbox.spring.data.model.Role;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.SessionAttributes;

@RestController
@SessionAttributes("message")
class NotificationController {

    private final RoleRepository repository;

    NotificationController(final RoleRepository repository) {
        this.repository = repository;
    }

    @PostMapping("/roles/{id}")
    Role updateRole(@PathVariable("id") final Integer id, @RequestBody final Role role) {
        final Role existing = repository.getOne(id);
        existing.setName(role.getName());
        existing.setPrivileges(role.getPrivileges());
        return repository.save(existing);
    }
}
