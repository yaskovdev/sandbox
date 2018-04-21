package com.yaskovdev.sandbox.spring.data;

import com.yaskovdev.sandbox.spring.data.model.Role;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
class RoleController {

    private final RoleService service;

    @Autowired
    RoleController(final RoleService service) {
        this.service = service;
    }

    @PostMapping("/roles")
    Role createRole(@RequestBody final Role role) {
        return service.createRole(role);
    }

    @GetMapping("/roles/{code}")
    Role getRoleByCode(@PathVariable("code") final String code) {
        return service.getRoleByCode(code);
    }

    @PostMapping("/roles/{code}")
    Role updateRole(@PathVariable("code") final String code, @RequestBody final Role role) {
        return service.updateRoleByCode(code, role);
    }
}
