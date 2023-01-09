package com.yaskovdev.gp.push;

import java.util.ArrayList;
import java.util.List;

public class Tokenizer {

    public List<Token> tokenize(final String program) {
        int position = 0;
        final List<Token> tokens = new ArrayList<>();
        while (position < program.length()) {
            if (isToken(program.charAt(position))) {
                final StringBuilder word = new StringBuilder();
                while (isToken(program.charAt(position))) {
                    word.append(program.charAt(position));
                    position++;
                }
                tokens.add(new Token(word.toString()));
            } else if (program.charAt(position) == '(' || program.charAt(position) == ')') {
                tokens.add(new Token(Character.toString(program.charAt(position))));
                position++;
            } else if (Character.isWhitespace(program.charAt(position))) {
                position++;
            } else {
                throw new RuntimeException("Unexpected character at position " + position + ": " + program.charAt(position));
            }
        }
        return tokens;
    }

    private static boolean isToken(char character) {
        return Character.isLetterOrDigit(character) || character == '.' || character == '_' || character == '<';
    }
}
