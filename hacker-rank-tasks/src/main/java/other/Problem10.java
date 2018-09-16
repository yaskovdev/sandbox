package other;

import java.util.ArrayList;
import java.util.List;

class Problem10 {

    boolean isMatch(String s, String p) {
        System.out.println(toGroups(p));
        return process(s, toGroups(p));
    }

    private boolean process(final String input, List<Group> groups) {
        if (groups.isEmpty() && input.isEmpty()) {
            return true;
        } else if (!groups.isEmpty() && input.isEmpty()) {
            return false;
        } else if (groups.isEmpty() && !input.isEmpty()) {
            return false;
        } else {
            final Group g = groups.get(0);
            int i = 0;
            while (i <= input.length() && g.isMatchedBy(input.substring(0, i))) {
                i++;
            }
            if (g.isFullyMatchedBy(input.substring(0, i - 1))) {
                return process(input.substring(i - 1), groups.subList(1, groups.size()));
            } else {
                return false;
            }
        }
    }

    private List<Group> toGroups(final String pattern) {
        final List<Group> groups = new ArrayList<>();
        final StringBuilder buffer = new StringBuilder();
        for (int i = 0; i < pattern.length(); i++) {
            final char c = pattern.charAt(i);
            if (Character.isLetter(c) || c == '.') {
                buffer.append(c);
            } else if (c == '*') {
                final String word = buffer.substring(0, buffer.length() - 1);
                if (!word.isEmpty()) {
                    groups.add(new Group(word));
                }
                groups.add(new Group(buffer.charAt(buffer.length() - 1) + "*"));
                buffer.setLength(0);
            } else {
                throw new RuntimeException("unexpected character " + c);
            }
        }
        if (buffer.length() > 0) {
            groups.add(new Group(buffer.toString()));
        }
        return groups;
    }
}
