package com.yaskovdev.sandbox.spring.transaction;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

@Service
class RoleServiceImpl implements RoleService {

	private static final Logger logger = LoggerFactory.getLogger(RoleServiceImpl.class);

	private final RoleRepository repository;
	private final RoleChangedService roleChangedService;

	@Autowired
	RoleServiceImpl(final RoleRepository repository, final RoleChangedService roleChangedService) {
		this.repository = repository;
		this.roleChangedService = roleChangedService;
	}

	@Override
	public Role createRole(final Role role) {
		return repository.save(role);
	}

	@Override
	public Role reproduceIssue(final String code) {
		logger.info("Going to read for the first time");
		repository.findOneByCode(code);
		roleChangedService.onRoleChanged(code);
		return null;
	}
}
