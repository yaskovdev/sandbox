package com.yaskovdev.gp.push.node;

import lombok.Data;

import java.util.List;

@Data
public class ProgramNode implements Node {

    private final List<Node> instructions;

    public List<Node> getInstructions() {
        return instructions;
    }
}
