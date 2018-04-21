package com.yaskovdev.sandbox.spring.data;

import com.yaskovdev.sandbox.spring.data.model.Role;

public interface RoleService {

    Role createRole(final Role role);

    Role getRoleByCode(final String code);

    Role updateRoleByCode(final String code, final Role role);
}
