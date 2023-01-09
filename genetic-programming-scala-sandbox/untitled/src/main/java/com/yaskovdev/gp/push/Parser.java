package com.yaskovdev.gp.push;

import com.yaskovdev.gp.push.node.FloatNode;
import com.yaskovdev.gp.push.node.InstructionNode;
import com.yaskovdev.gp.push.node.Node;
import com.yaskovdev.gp.push.node.ProgramNode;

import java.util.ArrayList;
import java.util.List;

import static java.lang.Double.parseDouble;

public class Parser {

    private int position;

    public List<Node> parseProgram(List<Token> program) {
        final List<Node> result = new ArrayList<>();
        while (position < program.size() && program.get(position).isNotCloseBracket()) {
            if (program.get(position).isOpenBracket()) {
                position++;
                final List<Node> instructions = parseProgram(program);
                if (program.get(position).isNotCloseBracket()) {
                    throw new RuntimeException("Expected ')', got " + program.get(position));
                }
                result.add(new ProgramNode(instructions));
            } else if (program.get(position).isInstruction()) {
                result.add(new InstructionNode(program.get(position).getValue()));
            } else {
                result.add(new FloatNode(parseDouble(program.get(position).getValue())));
            }
            position++;
        }
        return result;
    }
}
