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

    private int id;
    private String name;
    private Set<Privilege> privileges;

    public Role() {

    }

    public Role(String name) {
        this.name = name;
    }

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
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
                "Category[id=%d, name='%s']%n",
                id, name));
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
