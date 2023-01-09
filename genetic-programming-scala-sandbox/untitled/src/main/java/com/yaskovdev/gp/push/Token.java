package com.yaskovdev.gp.push;

import lombok.Data;

@Data
public class Token {

    private final String value;

    public boolean isInstruction() {
        return value.contains("_"); // TODO: use more robust check.
    }

    public boolean isOpenBracket() {
        return "(".equals(value);
    }

    public boolean isCloseBracket() {
        return ")".equals(value);
    }

    public boolean isNotCloseBracket() {
        return !isCloseBracket();
    }

    @Override
    public String toString() {
        return value;
    }
}
