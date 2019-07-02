package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Role;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.transaction.annotation.Transactional;

import static javax.persistence.LockModeType.PESSIMISTIC_WRITE;

interface RoleRepository extends JpaRepository<Role, Integer> {

	@Transactional(readOnly = true)
	Role findOneByCode(final String code);

	@Lock(PESSIMISTIC_WRITE)
	Role findOneAndLockByCode(final String code);
}
