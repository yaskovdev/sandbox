package other;

import org.junit.Test;

public class Problem426Test {

    @Test
    public void test() {
        final Node list = new Problem426().treeToDoublyList(
                new Node(8,
                        new Node(3, new Node(2, null, null), new Node(5, null, null)),
                        new Node(10, new Node(9, null, null), new Node(11, null, null))
                ));
        System.out.println(list);
    }

    @Test
    public void test2() {
        final Node list = new Problem426().treeToDoublyList(
                new Node(90,
                        new Node(-86, null,
                                new Node(66, new Node(-11, null, null), new Node(68, null, null))),
                        new Node(98, null, null)
                ));
        System.out.println(list);
    }

    @Test
    public void test3() {
        final Node list = new Problem426().treeToDoublyList(
                new Node(90,
                        null,
                        new Node(98, null, new Node(100, null, null))
                ));
        System.out.println(list);
    }

    @Test
    public void test4() {
        final Node list = new Problem426().treeToDoublyList(
                new Node(90,
                        new Node(80, new Node(70, null, null), null),
                        null
                ));
        System.out.println(list);
    }

    @Test
    public void test5() {
        final Node list = new Problem426().treeToDoublyList(
                new Node(90,
                        new Node(-86, null, new Node(66, new Node(-11, null, null), null)),
                        null
                ));
        System.out.println(list);
    }

}