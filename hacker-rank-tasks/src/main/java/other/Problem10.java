package other;

import java.util.ArrayList;
import java.util.List;

public class Problem10 {

    public boolean isMatch(String s, String p) {
        System.out.println(toGroups(p));
        return false;
    }

    private List<Group> toGroups(final String pattern) {
        final List<Group> groups = new ArrayList<>();
        String soFar = "";
        for (int i = 0; i < pattern.length(); i++) {
            final char c = pattern.charAt(i);
            if (Character.isLetter(c)) {
                Character afterNext = afterNext(pattern, i);
                if (afterNext == null) {
                    soFar += c;
                } else if (afterNext == '*') {
                    groups.add(new Group(soFar));
                    soFar = c + "";
                } else {
                    soFar += c;
                }
            } else if (c == '*') {
                soFar += c;
                groups.add(new Group(soFar));
                soFar = "";
            } else if (c == '.') {
                Character next = next(pattern, i);
                if (next != null && next == '*') {
                    soFar += c;
                } else {
                    soFar += c;
                    groups.add(new Group(soFar));
                    soFar = "";
                }
            } else {
                throw new RuntimeException("unexpected character " + c);
            }
        }
        return groups;
    }

    private static Character next(String pattern, int i) {
        return i + 1 < pattern.length() ? pattern.charAt(i + 1) : null;
    }

    private Character afterNext(String pattern, int i) {
        return i + 2 < pattern.length() ? pattern.charAt(i + 2) : null;
    }
}
