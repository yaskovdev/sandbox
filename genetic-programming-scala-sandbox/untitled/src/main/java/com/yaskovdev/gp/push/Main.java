package com.yaskovdev.gp.push;

import com.yaskovdev.gp.push.MainModule.GpConfig;
import com.yaskovdev.gp.push.MainModule.Program;
import com.yaskovdev.gp.push.node.Node;

import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        final List<Token> tokens = new Tokenizer().tokenize("((((((((((EXEC_YANKDUP ()) (INTEGER_STACKDEPTH INTEGER_<) ((CODE_STACKDEPTH BOOLEAN_STACKDEPTH CODE_NOOP) EXEC_SWAP ((INTEGER_STACKDEPTH EXEC_YANK) FLOAT_ADD ()) (EXEC_S)) (EXEC_S) CODE_NOOP) (CODE_STACKDEPTH) (BOOLEAN_STACKDEPTH BOOLEAN_STACKDEPTH EXEC_SWAP CODE_DOSTAR EXEC_S) EXEC_S ())) BOOLEAN_STACKDEPTH)) (CODE_STACKDEPTH EXEC_YANKDUP EXEC_SWAP)) EXEC_S)) CODE_NOOP)");
        System.out.println(tokens);
        final List<Node> nodes = new Parser().parseProgram(tokens);
        MainModule.runRush(MainModule.createProgram(nodes), MainModule.createState(), true);
    }
}
