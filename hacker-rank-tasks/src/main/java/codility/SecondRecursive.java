package codility;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import static java.lang.Math.abs;
import static java.lang.Math.pow;
import static java.util.Collections.emptyList;
import static java.util.Collections.singletonList;

public class SecondRecursive {

    public static void main(final String[] args) {
        final int size = 10000;
        final Random random = new Random();
        final List<Integer> input = new ArrayList<>(size);
        for (int i = 0; i < size; i++) {
            input.add(random.nextInt(2));
        }
        final int value = calculate(input);
        System.out.println(convert(-value, -2));
    }

    private static int calculate(final List<Integer> base) {
        return calculate(base, 0);
    }

    private static int calculate(final List<Integer> base, final int position) {
        return position > base.size() - 1 ?
                0 : base.get(position) * (int) pow(-2, position) + calculate(base, position + 1);
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
