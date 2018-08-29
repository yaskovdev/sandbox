package com.yaskovdev.sandbox.spring.transaction;

import com.yaskovdev.sandbox.spring.transaction.model.Role;

interface RoleService {

    Role createRole(Role role);

    Role reproduceIssue(final String code);
}
