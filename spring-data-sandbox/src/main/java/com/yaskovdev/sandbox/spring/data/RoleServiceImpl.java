package com.yaskovdev.sandbox.spring.data;

import com.yaskovdev.sandbox.spring.data.model.Privilege;
import com.yaskovdev.sandbox.spring.data.model.Role;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Set;

import static com.google.common.base.MoreObjects.firstNonNull;
import static java.util.Collections.emptySet;

@Service
public class RoleServiceImpl implements RoleService {

    private final RoleRepository repository;

    @Autowired
    public RoleServiceImpl(final RoleRepository repository) {
        this.repository = repository;
    }

    @Override
    @Transactional
    public Role createRole(final Role role) {
        restoreManyToOneMapping(role);
        return repository.save(role);
    }

    @Override
    @Transactional(readOnly = true)
    public Role getRoleByCode(final String code) {
        return repository.findOneByCode(code);
    }

    @Override
    @Transactional
    public Role updateRoleByCode(final String code, final Role role) {
        final Role existing = repository.findOneByCode(code);
        existing.setName(role.getName());
        setPrivileges(existing.getPrivileges(), role.getPrivileges());
        restoreManyToOneMapping(existing);
        return repository.save(existing);
    }

    private static void setPrivileges(final Set<Privilege> existing, final Set<Privilege> privileges) {
        existing.clear();
        existing.addAll(firstNonNull(privileges, emptySet()));
    }

    private static void restoreManyToOneMapping(final Role role) {
        role.getPrivileges().forEach(p -> p.setRole(role));
    }
}
