package codility;

import java.util.ArrayList;
import java.util.List;

import static java.lang.Math.abs;
import static java.lang.Math.pow;
import static java.util.Arrays.asList;
import static java.util.Collections.emptyList;
import static java.util.Collections.singletonList;

public class Second {

    public static void main(final String[] args) {
        final List<Integer> input = new ArrayList<>();
        for (int i = 0; i < 10_000; i++) {
            input.addAll(asList(1, 0, 0, 1, 1, 1, 0, 0, 0, 1));
        }
        final int value = calculate(input);
        System.out.println(value);
        System.out.println(convert(-value, -2));
    }

    private static int calculate(final List<Integer> base) {
        int result = 0;
        for (int position = 0; position < base.size(); position++) {
            result += base.get(position) * (int) pow(-2, position);
        }
        return result;
    }

    private static List<Integer> convert(final int input, final int base) {
        if (input == 0) {
            return emptyList();
        } else {
            final int m = abs(input % base);
            return concat(singletonList(m % 2), convert((input - m) / base, base));
        }
    }

    private static List<Integer> concat(final List<Integer> first, final List<Integer> second) {
        final List<Integer> result = new ArrayList<>(first.size() + second.size());
        result.addAll(first);
        result.addAll(second);
        return result;
    }
}
