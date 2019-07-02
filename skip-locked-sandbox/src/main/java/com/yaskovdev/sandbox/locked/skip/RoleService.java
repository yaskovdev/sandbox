package com.yaskovdev.sandbox.locked.skip;

import com.yaskovdev.sandbox.locked.skip.model.Role;

interface RoleService {

    Role createRole(Role role);

    Role reproduceIssue(final String code);
}
