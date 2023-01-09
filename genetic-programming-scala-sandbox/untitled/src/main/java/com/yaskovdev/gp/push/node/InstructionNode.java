package com.yaskovdev.gp.push.node;

import lombok.Data;

@Data
public class InstructionNode implements Node {

    private final String name;

    public String getName() {
        return name;
    }
}
