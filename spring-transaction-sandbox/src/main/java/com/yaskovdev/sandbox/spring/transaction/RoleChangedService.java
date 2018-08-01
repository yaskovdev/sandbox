package com.yaskovdev.sandbox.spring.transaction;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

@Service
class RoleChangedService {

	private final RoleRepository roleRepository;

	@Autowired
	RoleChangedService(final RoleRepository roleRepository) {
		this.roleRepository = roleRepository;
	}

	@Transactional
	public void onRoleChanged(final String code) {
		Role resource = roleRepository.findOneByCode(code);
		resource.setName(null);
		roleRepository.saveAndFlush(resource);
	}
}
