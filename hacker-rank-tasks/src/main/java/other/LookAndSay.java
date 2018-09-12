package other;

import java.util.ArrayList;
import java.util.List;

public class LookAndSay {

    List<String> lookAndSaySequence(final int n) {
        final List<String> sequence = new ArrayList<>(n);
        String input = "1";
        for (int i = 0; i < n; i++) {
            sequence.add(input);
            input = lookAndSay(input);
        }
        return sequence;
    }

    String lookAndSay(final String input) {
        StringBuilder output = new StringBuilder();
        char current = input.charAt(0);
        int count = 0;
        for (final char c : input.toCharArray()) {
            if (c == current) {
                count++;
            } else {
                output.append(count).append(current);
                current = c;
                count = 1;
            }
        }
        output.append(count).append(current);
        return output.toString();
    }
}
