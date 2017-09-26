package implementation;

import java.util.Scanner;

public class Kangaroo {

    private static String kangaroo(int x1, int v1, int x2, int v2) {
        if (x1 == x2) {
            return "YES";
        }
        int leftmostKangaroo = Math.min(x1, x2);
        int rightmostKangaroo = Math.max(x1, x2);
        final int leftmostKangarooSpeed = leftmostKangaroo == x1 ? v1 : v2;
        final int rightmostKangarooSpeed = rightmostKangaroo == x1 ? v1 : v2;
        if (leftmostKangarooSpeed <= rightmostKangarooSpeed) {
            return "NO";
        } else {
            while (leftmostKangaroo < rightmostKangaroo) {
                leftmostKangaroo += leftmostKangarooSpeed;
                rightmostKangaroo += rightmostKangarooSpeed;
                if (leftmostKangaroo == rightmostKangaroo) {
                    return "YES";
                }
            }
            return "NO";
        }
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int x1 = in.nextInt();
        int v1 = in.nextInt();
        int x2 = in.nextInt();
        int v2 = in.nextInt();
        String result = kangaroo(x1, v1, x2, v2);
        System.out.println(result);
    }
}
