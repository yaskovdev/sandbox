package com.yaskovdev.sandbox.spring.transaction;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.transaction.annotation.Transactional;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

interface RoleRepository extends JpaRepository<Role, Integer> {

	@Transactional
	Role findOneByCode(final String code);
}
