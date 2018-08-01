package com.yaskovdev.sandbox.spring.transaction;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

@Service
class RoleServiceImpl implements RoleService {

	private final RoleRepository repository;
	private final RoleChangedService roleChangedService;

	@Autowired
	RoleServiceImpl(final RoleRepository repository, RoleChangedService roleChangedService) {
		this.repository = repository;
		this.roleChangedService = roleChangedService;
	}

	@Override
	public Role createRole(final Role role) {
		return repository.save(role);
	}

	@Override
	public Role reproduceIssue(final String code) {
		repository.findOneByCode(code);
		roleChangedService.onRoleChanged(code);
		return null;
	}
}
