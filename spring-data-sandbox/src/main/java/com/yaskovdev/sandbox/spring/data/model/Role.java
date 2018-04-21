package com.yaskovdev.sandbox.spring.data.model;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import java.util.Set;

@Entity
public class Role {

    private Integer id;
    private String code;
    private String name;
    private Set<Privilege> privileges;

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getCode() {
        return code;
    }

    public void setCode(final String code) {
        this.code = code;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @OneToMany(mappedBy = "role", cascade = CascadeType.ALL)
    public Set<Privilege> getPrivileges() {
        return privileges;
    }

    public void setPrivileges(Set<Privilege> privileges) {
        this.privileges = privileges;
    }

    @Override
    public String toString() {
        StringBuilder result = new StringBuilder(String.format(
                "Role[id=%d, code='%s', name='%s']%n",
                id, code, name));
        if (privileges != null) {
            for (Privilege privilege : privileges) {
                result.append(String.format(
                        "Privilege[id=%d, name='%s']%n",
                        privilege.getId(), privilege.getName()));
            }
        }

        return result.toString();
    }
}
