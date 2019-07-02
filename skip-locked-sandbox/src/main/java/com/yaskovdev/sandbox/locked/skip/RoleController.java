package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Role;
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

	@GetMapping("/roles/{code}/issue")
	Role reproduceIssue(@PathVariable("code") final String code) {
		return service.reproduceIssue(code);
	}
}
