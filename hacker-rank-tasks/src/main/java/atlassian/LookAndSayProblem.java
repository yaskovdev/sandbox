package atlassian;

/**
 * Created by sergei.iaskov on 9/30/2017.
 */
public class LookAndSayProblem {

    public static void main(String[] args) {
        System.out.println(LookAndSay("11", 2));
    }

    static String LookAndSay(String start, int n) {
        if (n == 1) {
            return lookAndSay(start);
        } else {
            return LookAndSay(lookAndSay(start), n - 1);
        }
    }

    static String lookAndSay(String input) {
        final char[] chars = input.toCharArray();
        final StringBuilder result = new StringBuilder();
        char current = chars[0];
        int value = 0;
        for (char character : chars) {
            if (character == current) {
                value++;
            } else {
                result.append(value).append(current);
                value = 1;
                current = character;
            }
        }
        return result.append(value).append(current).toString();
    }
}
