package com.yaskovdev.sandbox.spring.transaction;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

@Service
class RoleChangedServiceImpl implements RoleChangedService {

	private static final Logger logger = LoggerFactory.getLogger(RoleServiceImpl.class);

	private final RoleRepository roleRepository;

	@Autowired
	RoleChangedServiceImpl(final RoleRepository roleRepository) {
		this.roleRepository = roleRepository;
	}

	@Override
	@Transactional
	public void onRoleChanged(final String code) {
		logger.info("Going to read for the second time");
		Role resource = roleRepository.findOneAndLockByCode(code);
		resource.setName(null);
		logger.info("Going to update");
		roleRepository.saveAndFlush(resource);
	}
}
