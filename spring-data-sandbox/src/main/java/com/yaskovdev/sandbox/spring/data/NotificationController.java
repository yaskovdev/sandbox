package com.yaskovdev.sandbox.spring.data;

import com.yaskovdev.sandbox.spring.data.model.Privilege;
import com.yaskovdev.sandbox.spring.data.model.Role;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.SessionAttributes;

import java.util.Set;

import static java.util.Collections.emptySet;
import static java.util.Optional.ofNullable;

@RestController
@SessionAttributes("message")
class NotificationController {

    private final RoleRepository repository;

    NotificationController(final RoleRepository repository) {
        this.repository = repository;
    }

    @PostMapping("/roles")
    Role createRole(@RequestBody final Role role) {
        restoreManyToOneMapping(role);
        return repository.save(role);
    }

    // TODO: should be transactional
    @PostMapping("/roles/{code}")
    Role updateRole(@PathVariable("code") final String code, @RequestBody final Role role) {
        final Role existing = repository.findOneByCode(code);
        existing.setName(role.getName());
        setPrivileges(existing.getPrivileges(), role.getPrivileges());
        restoreManyToOneMapping(existing);
        return repository.save(existing);
    }

    private static void setPrivileges(final Set<Privilege> existing, final Set<Privilege> privileges) {
        existing.clear();
        existing.addAll(ofNullable(privileges).orElse(emptySet())); // TODO: use guava
    }

    private static void restoreManyToOneMapping(final Role role) {
        role.getPrivileges().forEach(p -> p.setRole(role));
    }
}
