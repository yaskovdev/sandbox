package days14.bitwise;

import days14.Node;

import java.util.Collection;
import java.util.Collections;

public class GetBit {

    public static void main(String[] args) {
        final Collection<Node> nodes = Collections.emptyList();
        for (Node node : nodes) {
            process(node);
        }
        nodes.forEach(GetBit::process);
        Integer number = 13;
        final String bits = Integer.toBinaryString(number);
        System.out.println(bits);
        System.out.println(bit(number, 0));
        System.out.println(bit(number, 1));
        System.out.println(bit(number, 2));
        System.out.println(bit(number, 3));
    }

    private static void process(Node node) {

    }

    private static boolean bit(final int number, final int index) {
        return ((number >>> index) & 1) == 1;
    }
}
