package com.yaskovdev.gp.push.node;

import lombok.Data;

@Data
public class FloatNode implements Node {

    private final double value;

    public double getValue() {
        return value;
    }
}
