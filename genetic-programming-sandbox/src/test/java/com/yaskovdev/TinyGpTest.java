package com.yaskovdev;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class TinyGpTest {

    @Test
    public void test() {
        final TinyGp instanceUnderTest = new TinyGp("/Users/yaskovdev/dev/git_home/sandbox/genetic-programming-sandbox/target/classes/sin-data.txt", 0);
//        instanceUnderTest.print_indiv("roq\u000BoqN$qoM\u0018\u0019nnTr\u0002\u0000qp\u0019\u00007".toCharArray(), 0);
        final char[] chars = "roq\u000BoqN$qoM\u0018\u0019nnTr\u0002\u0000qp\u0019\u00007".toCharArray();
        for (char c : chars) {
            System.out.println((int) c);
        }
        instanceUnderTest.print_indiv(new char[] {114, 114, 1}, 0);
    }

}