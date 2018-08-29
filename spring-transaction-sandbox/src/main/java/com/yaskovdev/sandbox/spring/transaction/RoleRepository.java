package com.yaskovdev.sandbox.spring.transaction;

import static javax.persistence.LockModeType.PESSIMISTIC_WRITE;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.transaction.annotation.Transactional;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

interface RoleRepository extends JpaRepository<Role, Integer> {

	@Transactional(readOnly = true)
	Role findOneByCode(final String code);

	@Lock(PESSIMISTIC_WRITE)
	Role findOneAndLockByCode(final String code);
}
