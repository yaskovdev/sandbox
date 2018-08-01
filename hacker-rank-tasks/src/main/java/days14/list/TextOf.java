package days14.list;

import java.util.ArrayList;
import java.util.List;

class TextOf {

    private final Node node;

    TextOf(final Node node) {
        this.node = node;
    }

    @Override
    public String toString() {
        final List<Integer> values = new ArrayList<>();
        Node node = this.node;
        while (node != null) {
            values.add(node.data);
            node = node.next;
        }
        return values.toString();
    }
}
